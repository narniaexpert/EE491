# B3 (Beaglebone Black) Pin Initialization

class B3_Initialize
  def initialize(pin_1, pin_2, pin_3, pin_4, pin_5, pin_6, pin_7, pin_8, pin_9, pin_10, pin_11, pin_12, pin_13, pin_14, pin_15, pin_16, pin_17, pin_18, pin_19, pin_20,
  pin_21, pin_22, pin_23, pin_24, pin_25, pin_26, pin_27, pin_28, pin_29, pin_30, pin_31, pin_32, pin_33, pin_34, pin_35, pin_36, pin_37, pin_38, pin_39, pin_40,
  pin_41, pin_42, pin_43, pin_44, pin_45, pin_46)
  
  @pin_1 = 0  
	@pin_2 = 0
	@pin_3 = 0 #GPI01_6
	@pin_4 = 0 #GPI01_7
	@pin_5 = 0 #GPI01_2
	@pin_6 = 0 #GPI01_3
	@pin_7 = 0 #TIMER4
	@pin_8 = 0 #TIMER7
	@pin_9 = 0 #TIMER5
	@pin_10 = 0 #TIMER6
  @pin_11 = 0 #GPI01_13
	@pin_12 = 0 #GPI01_12
	@pin_13 = 0 #EHRPWM2B
	@pin_14 = 0 #GPI00_26
	@pin_15 = 0 #GPI01_15
	@pin_16 = 0 #GPI01_14
	@pin_17 = 0 #GPI00_27
	@pin_18 = 0 #GPI02_1
	@pin_19 = 0 #EHRPWM2A
	@pin_20 = 0 #GPI01_31
  @pin_21 = 0 #GPI01_30
	@pin_22 = 0 #GPI01_5
	@pin_23 = 0 #GPI01_4
	@pin_24 = 0 #GPI01_1
	@pin_25 = 0 #GPI01_0
	@pin_26 = 0 #GPI01_29
	@pin_27 = 0 #GPI02_22
	@pin_28 = 0 #GPI02_24
	@pin_29 = 0 #GPI02_23
	@pin_30 = 0 #GPI02_25
  @pin_31 = 0 #UART5_CTSN
	@pin_32 = 0 #UART5_RTSN
	@pin_33 = 0 #UART4_RTSN
	@pin_34 = 0 #UART3_RTSN
	@pin_35 = 0 #UART4_CTSN
	@pin_36 = 0 #UART3_CTSN
	@pin_37 = 0 #UART5_TXD
	@pin_38 = 0 #UART5_RXD
	@pin_39 = 0 #GPI02_12
	@pin_40 = 0 #GPI02_13
  @pin_41 = 0 #GPI02_10
	@pin_42 = 0 #GPI02_11
	@pin_43 = 0 #GPI02_8
	@pin_44 = 0 #GPI02_9
	@pin_45 = 0 #GPI02_6
	@pin_46 = 0 #GPI02_7
  end
  puts "All Pins Initialized!"
end
  