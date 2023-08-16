=begin
-----
motor control
-----
101011 forward
010111 reverse
0010x1 left
10001x right

011011 left rotate
100111 right rotate
111111 halt
xxxx00 freerun

-----
Turret control
-----
3.3V
@50Hz
5% -9% - 13%
min - user min - max

---add turret portion---

=end

cycle_time = 0.02
Duty_Cycle = 0.05 # max = 9%

def check
	falsecount = 5
	while  falsecount > 4
		delay_seconds = cycle_time
		turret_movement(T1,T2)
	end
end

def turret_movement(T1,T2) 
	if T1 == 1 && T2 == 0   # raise height
		if Duty_Cycle >= 0.09
			Duty_Cycle += 0.0025
		end
	end
	if T1 == 0 && T2 == 1 # lower height
		if Duty_Cycle <= 0.05
			Duty_Cycle -= 0.0025
		end
	end

	digital_writer = 14,HIGH
	uSDelay = cycle_time*Duty_Cycle
	delay_microseconds = uSDelay
	digital_write = 14,LOW
	delay_microseconds = cycle_time-uSDelay
end


