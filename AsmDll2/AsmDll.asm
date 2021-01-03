
;==================== ARGUMENTS=======================
;RCX - address od first proceeding element of src array
;RDX - address of first element of dst array
;XMM2 - ratio (edge of the src bitmap / edge of the dst bitmap)
;XMM3 - just a placeholder, which solves my problem with stack
;R8 - height of the src bitmap
;R9 - height of the dst bitmap
;R10 - index of first column to draw
;R11 - number of columns to draw
;======================================================

.CODE

ImageExtenderAsm PROC
		
		MOV R15, RDX									;Move addres of dst array to R15

		MOV R8, QWORD PTR [RSP + 40]					;Saving arguments from stack to registers
		MOV R9, QWORD PTR [RSP + 48]					;
		MOV R10, QWORD PTR [RSP + 56]					;
		MOV R11, QWORD PTR [RSP + 64]					;

														;Calculating current dst array pixel
		MOV RAX, R9										;Move height of the dst bitmap to RAX, to firther multiplication
		MUL R10											;Multiply by the index of first column to draw
		MOV RDX, 4										;Every pixel has 4 components, so I have to multiply it by 4
		MUL RDX											; 
		ADD R15, RAX									;Adding calculated number to starting address

		ADD R11, R10									;Calculating ending loop condition
														;Outer loop ends when R11 == R10
		
		ColumnLoop: 
			
			MOVD XMM0, R10								;Multiplying current column number by ratio
			MULPS XMM0, XMM2							;
			MOVD R12D, XMM0								;
			
			MOV R14, 0									;Inner loop iterator

			RowLoop: 
				
				MOVD XMM0, R14							;Multiplying current row by ratio
				MULPS XMM0, XMM2						;
				MOVD R13D, XMM0							;

														;Calculating currrent pixel from src array
				MOV eax, R8D							;Moving height of the src bitmap to eac
				MUL R12									;Multipling by number of proceeded columns
				ADD eax, R13D							;Adding number of proceeded rows
				MOV RDX, 4								;Every pixel has 4 components, so I have to multiply it by 4
				MUL RDX									;
				MOV RDX, R8								;Save R8 content for now
				MOV R8D, eax							;Loading number of pixels which have to be skipped to R8 register


				MOV AL, [RCX+R8]						;Get current pixel value to AL
				MOV [R15], AL							;Save the value to current address of dst array
				INC R15									;Jump to next dst array address
				INC R8									;Jump to next pixel component

				MOV AL, [RCX+R8]
				MOV [R15], AL 
				INC R15 
				INC R8 

				MOV AL, [RCX+R8] 
				MOV [R15], AL 
				INC R15 
				INC R8 

				MOV AL, [RCX+R8] 
				MOV [R15], AL 
				INC R15 
				

				MOV R8, RDX								;Restore R8 content

				INC R14									;Increment row iterator
				CMP R14, R9								;Check ending condition
				JNZ RowLoop								
		
			INC R10										;Increment column iterator
			CMP R11, R10								;Check ending condition
			JNZ ColumnLoop 

		RET 

ImageExtenderAsm ENDP
 
END