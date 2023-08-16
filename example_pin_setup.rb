#Arduino-like library for Ruby on BeagleBone

HIGH = "HIGH"
LOW = "LOW"
OUTPUT = "OUTPUT"
INPUT = "INPUT"
pinList = [] 
startTime = time.time() # needed for millis(). Look up Time class in rubydoc

=begin
the following is the pin initialization for the BBB
we will do something very similar to this once we get the JRuby 
on the BBB. We need to install the gem in th bin file an then 
initialize the following in a similar pattern
=end

digitalPinDef = {
			"P8.3":		38,
			"P8.4":		39,
			"P8.5":		34,
			"P8.6":		35,
			"P8.7":		66,
			"P8.8":		67,
			"P8.9":		69,
			"P8.10":	68,
			"P8.11":	45,
			"P8.12":	44,
			"P8.13":	23,
			"P8.14":	26,
			"P8.15":	47,
			"P8.16":	46,
			"P8.17":	27,
			"P8.18":	65,
			"P8.19":	22,
			"P8.20":	63,
			"P8.21":	62,
			"P8.22":	37,
			"P8.23":	36,
			"P8.24":	33,
			"P8.25":	32,
			"P8.26":	61,
			"P8.27":	86,
			"P8.28":	88,
			"P8.29":	87,
			"P8.30":	89,
			"P8.31":	10,
			"P8.32":	11,
			"P8.33":	9,
			"P8.34":	81,
			"P8.35":	8,
			"P8.36":	80,
			"P8.37":	78,
			"P8.38":	79,
			"P8.39":	76,
			"P8.40":	77,
			"P8.41":	74,
			"P8.42":	75,
			"P8.43":	72,
			"P8.44":	73,
			"P8.45":	70,
			"P8.46":	71,
			"P9.11":	30,
			"P9.12":	60,
			"P9.13":	31,
			"P9.14":	50,
			"P9.15":	48,
			"P9.16":	51,
			"P9.17":	5,
			"P9.18":	4,
			"P9.19":	13,
			"P9.20":	12,
			"P9.21":	3,
			"P9.22":	2,
			"P9.23":	49,
			"P9.24":	15,
			"P9.25":	117,
			"P9.26":	14,
			"P9.27":	115,
			"P9.28":	113,
			"P9.29":	111,
			"P9.30":	112,
			"P9.31":	110,
			"P9.41":	20,
			"P9.42":	7}

analogPinDef = {
			"P9.33":	"ain4",
			"P9.35":	"ain6",
			"P9.36":	"ain5",
			"P9.37":	"ain2",
			"P9.38":	"ain3",
			"P9.39":	"ain0",
			"P9.40":	"ain1"}

def pinMode(pin, direction):
	#pinMode(pin, direction) opens (exports) a pin for use, and sets the direction
	if pin in digitalPinDef: # if we know how to refer to the pin:
		fw = file("/sys/class/gpio/export", "w")
		fw.write("%d" % (digitalPinDef[pin])) # write the pin to export to userspace
		fw.close()
		fileName = "/sys/class/gpio/gpio%d/direction" % (digitalPinDef[pin]) 
		fw = file(fileName, "w")
		if direction == INPUT: 
			fw.write("in") # write the diretion
		else:
			fw.write("out") # write the diretion
		fw.close()
		pinList.append(digitalPinDef[pin]) # Keep a list of exported pins.
	else: #if we don't know how to refer to a pin:
		p "pinMode error: Pin #{pin}"


def digitalWrite(pin, status):
	#digitalWrite(pin, status) sets a pin HIGH or LOW
	if digitalPinDef[pin] in pinList: # check if we exported the pin in pinMode
		fileName = "/sys/class/gpio/gpio%d/value" % (digitalPinDef[pin])
		fw = file(fileName, "w") # open the pin's value file for writing
		if status == HIGH:
			fw.write("1") # Set the pin HIGH by writing 1 to its value file
		if status == LOW:
			fw.write("0") # Set the pin LOW by writing 0 to its value file
		fw.close()
	else: # if we haven't exported the pin, print an error:
		print "digitalWrite error: Pin mode for #{pin}"
	
def digitalRead(pin):
	#digitalRead(pin) returns HIGH or LOW for a given pin.
	if digitalPinDef[pin] in pinList: # check if we exported the pin in pinMode
		fileName = "/sys/class/gpio/gpio%d/value" % (digitalPinDef[pin])
		fw = file(fileName, "r") # open the pin's value file for reading
		inData = fw.read()
		fw.close()
		if inData == "0\n": # a 0 means it's low
			return LOW
		if inData == "1\n": # a 1 means it's high
			return HIGH
	else: # if we haven't exported the pin, print an error (not working for some reason):
		print "digitalRead error: Pin mode for #{pin}"
		return -1;

	
def pinUnexport(pin): # helper function for cleanup()
	#pinUnexport(pin) closes a pin in sysfs. This is usually called by cleanup() when a script is exiting.
	fw = file("/sys/class/gpio/unexport", "w")
	fw.write("%d" % (pin))
	fw.close()

def delay(millis):
	time.sleep(millis/1000.0)

def millis():
	return int((time.time() - startTime) * 1000) #look up Time class from rubydoc

def run(setup, main): #basic try catch
	try:
		setup()
		while (True):
			main()
	except KeyboardInterrupt:
	except Exception, e:
    # Something may have gone wrong, clean up and print exception
		print e #prints exception
