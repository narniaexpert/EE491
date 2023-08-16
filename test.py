import time 
import Adafruit_BBIO.GPIO as GPIO
GPIO.setup("P8_12", GPIO.IN)
GPIO.setup("P8_13", GPIO.IN)
GPIO.setup("P8_14", GPIO.IN)
GPIO.setup("P8_15", GPIO.IN)
GPIO.setup("P8_16", GPIO.OUT)
GPIO.setup("P8_17", GPIO.OUT)
GPIO.setup("P8_18", GPIO.OUT)
GPIO.setup("P8_19", GPIO.OUT)
GPIO.output("P8_16", GPIO.LOW)
GPIO.output("P8_17", GPIO.LOW)
GPIO.output("P8_18", GPIO.LOW)
GPIO.output("P8_19", GPIO.LOW)
while True:
	if GPIO.input("P8_12"):
		GPIO.output("P8_16", GPIO.HIGH)
	else:
		GPIO.output("P8_16", GPIO.LOW)

	if GPIO.input("P8_13"):
		GPIO.output("P8_17", GPIO.HIGH)
	else:
		GPIO.output("P8_17", GPIO.LOW)

	if GPIO.input("P8_14"):
		GPIO.output("P8_18", GPIO.HIGH)
	else:
		GPIO.output("P8_18", GPIO.LOW)

	if GPIO.input("P8_15"):
		GPIO.output("P8_19", GPIO.HIGH)
	else:
		GPIO.output("P8_19", GPIO.LOW)
	time.sleep(.5)

