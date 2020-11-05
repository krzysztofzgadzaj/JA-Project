;autor Krzysztof Zgadzaj

.code

;========================= ARGUMENTS ==============================
;RCX - pointer to source array
;RDX - pointer to dst array
;XMM2 - ratio
;R8 - width of src array
;R9 - width of dst array
;R10 - number of lines to change
;R11 - index of first line to change
;==================================================================

;=========== Register Description =================================
;R10 - max line Index
;R11 - height counter
;R12 - width counter
;R13 - ratio x X
;R14 - ratio x Y 
;R15 - RDX
;==================================================================


filterProc PROC

			MOV R10, QWORD PTR [RSP + 40]                        ;Loading number of lines from stack
            MOV R11, QWORD PTR [RSP + 48]                        ;Loading index of first line from stack
            MOV R15, QWORD PTR [RSP + 56]                        ;Loading src image width
          
            mov r8, r15                                          ;Load src image width to R8
            mov R15, RDX                                         ;Move dst ptr

            add R10, R11                                         ;Set max index of line in R10

            ;TODO - set dst pointer, maybe in c#
            
        RowLoop: 

            movd xmm1, R11                                       ;Calculate ratio x Y
            mulps xmm1, xmm2
            movd R13D, xmm1

            mov eax, r13d                                       ;Calculate przesuniecie rcx, bez xRatio
            mul r8
            mov r13d, eax                                        ;Przesuniecie bez xRatio
            
            mov R12, 0                                           ;Width counter
           
            ColumnLoop: 
                
            
                movd xmm1, R12                                   ;Calculate ratio x X
                mulps xmm1, xmm2
                movd R14D, xmm1

                add r14, r13                                 ;przesuniecie rcx

                mov R13, 4                                    ;Load 4 to register
                mov eax, r14d                                ;Multiply int * 4
                mul R13
                mov r14d, eax

                add r14, rcx

                mov BH, 4										 ;COunter for setPixelLoop

			setPixelLoop:	
            
					mov EAX, [R14]								 ;ading pixel form src to temp buffor
					mov [R15], EAX								 ;ading pixel from buffor to dst image
					inc R14										 ;2 + 1
					inc R15									     ;3 + 1

					dec BH                                       ;Decrement setPixelLoop counter
					cmp BH, 0                                    ;Check if BH == 0
					jg setPixelLoop
				               
                INC R12                                          ;Increment x counter
                CMP R12, R9                                      ;Check if x counter == width
                JNZ ColumnLoop                                   
        
            INC R11                                              ;Increment height counter
            CMP R10, R11                                         ;Compare height counter with number of lines to do
            JNZ RowLoop                                          

				RET										; return from procedure

filterProc endp

end
