;ASM Library that contains procedure to filter properly formated image with given 3x3 filter
;autor Mateusz Czarnecki

.code

;========================= ARGUMENTS ==============================
;RCX - pointer to array with filter [IntPtr 32bit]
;RDX - pointer to array with part of the image [IntPtr 32bit]
;R8 - length of array with part of the image [int 32bit]
;XMM3 - norm [float 32bit]
;==================================================================

;=========== Register Description =================================
;RCX - pointer to array with filter
;RDX - pointer to array with part of the image
;R8 - length of array with part of the image

;R14 - index of color array
;R15 - index of filter array

;XMM0 - sum RGB values of current part of the image |x|R|G|B|
;XMM1 - currelt pixel |x|R|G|B|
;XMM2 - current filter |x|filter|filter|filter|
;XMM3 - norm |x|norm|norm|norm|
;==================================================================


filterProc PROC

				mov r14, rdx
				
				mov r10, [rcx]
				mov r11, [rcx + 1]
				mov r12, [rcx + 2]
				mov r13, [rcx + 3]

				
			
			siema:
				

				mov [r14], r10
				
				inc r14

				mov [r14], r11
				inc r14

				mov [r14], r12
				inc r14

				mov [r14], r13
				inc r14

				sub r8, 1
				cmp r8, 0
				jg siema


				RET										; return from procedure

filterProc endp

end
