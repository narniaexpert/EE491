



import Adafruit_BBIO.PWM as PWM
import time


PWM.start("P9_14", 100, 200000)
time.sleep(1)
duty = 100
frequency = 200000

while frequency > 0:
	PWM.set_duty_cycle("P9_14", duty)
	PWM.set_frequency("P9_14", frequency)
	print("          Running")
	#duty = duty - 5
	frequency = frequency - 10000
	time.sleep(.5)
