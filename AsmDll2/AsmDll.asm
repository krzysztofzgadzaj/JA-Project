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
				

			start:
				mov R8, [RCX]	
				add R8, 15
				mov [RCX], R8

				add RCX, 4

				dec RDX

				cmp RDX, 0
				JNE start

				
				

				RET										; return from procedure

filterProc endp

end
