#!/usr/bin/ruby
frequency = 100

# pwm.rb

def Setup 
pin_mode = 13,OUTPUT
end

def Left_Timing_loop (Duty_Cycle_Left)
digital_writer = 13,HIGH
delay_microseconds = frequency*Duty_Cycle_Left
digital_write = 13,LOW
delay_microseconds = freqency-delay_microseconds
end

def Right_Timing_loop (Duty_Cycle_Right)
digital_writer = 12,HIGH
delay_microseconds = frequency*Duty_Cycle_Right
digital_write = 12,LOW
delay_microseconds = freqency-delay_microseconds
end

def check
  pseudocount = 5
  while pseudocount > 4
    delay_seconds = cycle_time
  end
end