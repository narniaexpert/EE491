# Motor_Control.rb

A1 = nil 
B1 = nil
A2 = nil
B2 = nil

PWMA = nil
PWMB = nil

if A1 == "1" do

# Should there be an "and" here?

if A2 == "1" do

move_forward

else

 turn_right

end

end

if A1 == "0" do

if B1 == "1" do

reverse_motion

else

turn_left

end

end