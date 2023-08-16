# Vehicle Movement

# def control_main(A1,A2,B1,B2,PWMA,PWMB)

A1 = 0
A2 = 0
B1 = 0
B2 = 0

PWMA = nil
PWMB = nil

if A1 == 1 
  if A2 == 1
    if B1 == 0
		# move_forward
    # move_forward(PWMA,PWMB)
    puts "moving forward"
  elsif A2 == 0
		# turn_right
    # turn_right(PWMA,PWMB)
    puts "turning right"
  end
	end
end

if A1 == 0 
  if B1 == 1
    if B2 == 1
		# move_reverse
    # move_reverae(PWMA,PWMB)
    puts "reverse"
  elsif B1 == 0
		# turn_left
    # turn_left(PWMA,PWMB)
    puts "turn left"
  end
	end
end

if A1 == 1
  if A2 == 1
    if B1 == 1
      if B2 == 1
        # stop_movement
        # stop_movement(PWMA,PWMB)
        puts "stop movement"
      end
    end
  end
end

if A1 == 0
  if A2 == 0
    if B1 == 0
      if B2 == 0
        # free_run
        # free_run(PWMA,PWMB)
        puts "free run"
      end
    end
  end
end

# end