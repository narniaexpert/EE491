import time 
import Adafruit_BBIO.GPIO as GPIO
GPIO.setup("P8_23", GPIO.IN)
GPIO.setup("P8_24", GPIO.IN)
GPIO.setup("P8_25", GPIO.IN)
GPIO.setup("P8_26", GPIO.IN)
GPIO.setup("P8_27", GPIO.OUT)
GPIO.setup("P8_28", GPIO.OUT)
GPIO.setup("P8_29", GPIO.OUT)
GPIO.setup("P8_30", GPIO.OUT)
GPIO.output("P8_27", GPIO.LOW)
GPIO.output("P8_28", GPIO.LOW)
GPIO.output("P8_29", GPIO.LOW)
GPIO.output("P8_30", GPIO.LOW)

def north_west():
	print("north_west")

def north():
	print("north")

def north_east():
	print("north_east")
	
def north_west_slow():
	print("north_west_slow")
	
def north_slow():
	print("north_slow")

def north_east_slow():
	print("north_east_slow")

def west():
	print("west")

def west_slow():
	print("west_slow")

def dont_move():
	print("dont_move")

def east_slow():
	print("east_slow")

def east():
	print("east")

def south_west():
	print("south_west")

def south_slow():
	print("south_slow")

def south_east():
	print("south_east")

def south():
	print("south")

options = { 
        "0000" : north_west,
	    "0001" : north ,
	    "0010" : north_east ,
	    "0011" : north_west_slow ,
	    "0100" : north_slow ,
	    "0101" : north_east_slow ,
	    "0110" : west ,
	    "0111" : west_slow ,
	    "1000" : dont_move ,
	    "1001" : east_slow ,
	    "1010" : east ,
	    "1011" : south_west ,
	    "1100" : south_slow ,
	    "1101" : south_east ,
	    "1110" : south ,
	    "1111" : dont_move 
	   }

while True:
	string = str(GPIO.input("P8_23")) +  str(GPIO.input("P8_24")) +  str(GPIO.input("P8_25")) +  str(GPIO.input("P8_26"))
	options[string]()
	time.sleep(.5)
    
	if GPIO.input("P8_23"):
		GPIO.output("P8_27", GPIO.HIGH)
	else:
		GPIO.output("P8_27", GPIO.LOW)

	if GPIO.input("P8_24"):
		GPIO.output("P8_28", GPIO.HIGH)
	else:
		GPIO.output("P8_28", GPIO.LOW)

	if GPIO.input("P8_25"):
		GPIO.output("P8_29", GPIO.HIGH)
	else:
		GPIO.output("P8_29", GPIO.LOW)

	if GPIO.input("P8_26"):
		GPIO.output("P8_30", GPIO.HIGH)
	else:
		GPIO.output("P8_30", GPIO.LOW)
	time.sleep(.5)

