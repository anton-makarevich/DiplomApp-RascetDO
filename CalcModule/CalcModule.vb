Public Class CalcModule
    'Переменные и классы для работы с номограммами
    Dim MN As New MAACommon.MAANomo          'класс библиотеки по работе с номограммами
    'Массив для загрузки номограмм
    Dim Nomo(1, 1) As Single
    'Public Nomo1(1, 1) As Single
    Dim TR As Double
    Dim Angles(16000, 10) As Double
    Dim AnglesF(9) As Single
    Dim AngCount As Integer
    Dim nomopath As String
    Dim blockReZ(7, 9) As Single
    Dim Rez(1, 4, 9) As Single
    Dim RezF(9) As Single

    Dim h1 As Single
    Dim h2 As Single
    Dim h3 As Single

    Dim e1 As Single
    Dim e2 As Single
    Dim e3 As Single
    Dim e4 As Single
    

    'results
    Dim Rast(4) As String
    Dim Szim(4) As String
    Dim Progib As String
    Dim mTau(4) As String
    Dim mSigma(4) As String
    

    Dim H2grp(1) As Integer
    Dim E1grp(1) As String
    Dim E3grp(1) As String
    Dim Osngrp(1) As String
    Dim osn(1) As Integer

    Dim blocks(7) As String
    Dim strdata As String
    Dim Etbl(11, 11) As String
    Dim SR As System.IO.StreamReader
    Dim SW, Sw2 As System.IO.StreamWriter

    Dim basePath As String 

    Public Sub New(ByVal nomoPath As String)
        basePath = nomoPath

         Dim i, j As Integer
        Dim l, m, n As Double
        Dim tarray() As String
        Dim fi As New System.IO.FileInfo(basePath+"\E.tbl")
        SR = fi.OpenText
        For i = 0 To 11
            strdata = SR.ReadLine
            tarray = Split(strdata, vbTab)
            For j = 0 To 11
                Etbl(i, j) = tarray(j)
            Next
        Next
        SR.Close()

        AngCount = 4
        Angles(1, 1) = Math.Cos(0)
        Angles(1, 2) = Math.Cos(Math.PI / 2)
        Angles(1, 3) = Math.Cos(Math.PI / 2)

        Angles(2, 1) = Math.Cos(Math.PI / 2)
        Angles(2, 2) = Math.Cos(0)
        Angles(3, 3) = Math.Cos(Math.PI / 2)

        Angles(3, 1) = Math.Cos(Math.PI / 2)
        Angles(3, 2) = Math.Cos(Math.PI / 2)
        Angles(3, 3) = Math.Cos(0)

        For l = 0 To Math.PI / 2 Step 0.01
            For m = 0 To Math.PI / 2 Step 0.01
                n = Math.Asin(Math.Sqrt(2 - Math.Sin(l) ^ 2 - Math.Sin(m) ^ 2))
                If n >= 0 And n <= Math.PI Then
                    If l <> Math.PI / 2 And m <> Math.PI / 2 And n <> Math.PI / 2 Then
                        Angles(AngCount, 1) = Math.Cos(l)
                        Angles(AngCount, 2) = Math.Cos(m)
                        Angles(AngCount, 3) = Math.Cos(n)
                        AngCount = AngCount + 1
                    End If
                End If
            Next m

        Next l
    End Sub

    Public Function Calculate(ByVal model As CommonClasses.PavementModel) As CommonClasses.PavementModel
        

        Dim R(1) As Single
        Dim blockcount, streSScount, sloIcount, h2Count, osnCount As Integer
        Dim SSl As String = "R"
        Dim rezTS As Single

        Dim DHB, DHM, DEB, DEM, DOB, DOM As Single

        h1=model.H1
        h2=model.H2
        h3=model.H3
        e1=model.E1
        e2=model.E2
        e3=model.E3
        e4=model.E4


        If h1 <= 1 Then h1 = 1.0001
        If h2 <= 1 Then h2 = 1.0001
        If h3 <= 1 Then h3 = 1.0001
        If h1 >= 10 Then h1 = 9.998
        If h2 >= 10 Then h2 = 9.998
        If h3 >= 10 Then h3 = 9.998
        If e1 <= 50 Then e1 = 50.0001
        If e2 <= 50 Then e2 = 50.0001
        If e3 <= 50 Then e3 = 50.0001
        If e4 < 50 Then e4 = 50
        If e1 >= 5000 Then e1 = 4999.9
        If e2 >= 5000 Then e2 = 4999.9
        If e3 >= 5000 Then e3 = 4999.9
        If e4 > 300 Then e4 = 300

        'блоки в зависимости от модуля основания
        If e4 < 150 Then
            osn(0) = 0
            osn(1) = 1
            Osngrp(0) = "50"
            Osngrp(1) = "150"
            DOB = 100
            DOM = e4 - 50
        Else
            osn(0) = 1
            osn(1) = 2
            Osngrp(0) = "150"
            Osngrp(1) = "300"
            DOB = 150
            DOM = e4 - 150
        End If
        'Определение блоков в зависимости от модулей слоев

        For osnCount = 0 To 1
            If e2 < 500 Then
                If e1 < 500 Then
                    If e3 < 500 Then
                        blocks(0) = Etbl(0, 0 + 4 * osn(osnCount))
                        blocks(1) = Etbl(0, 1 + 4 * osn(osnCount))
                        blocks(2) = Etbl(1, 0 + 4 * osn(osnCount))
                        blocks(3) = Etbl(1, 1 + 4 * osn(osnCount))
                        blocks(4) = Etbl(3, 0 + 4 * osn(osnCount))
                        blocks(5) = Etbl(3, 1 + 4 * osn(osnCount))
                        blocks(6) = Etbl(4, 0 + 4 * osn(osnCount))
                        blocks(7) = Etbl(4, 1 + 4 * osn(osnCount))
                        E3grp(0) = "50"
                        E3grp(1) = "500"
                    Else
                        blocks(0) = Etbl(1, 0 + 4 * osn(osnCount))
                        blocks(1) = Etbl(1, 1 + 4 * osn(osnCount))
                        blocks(2) = Etbl(2, 0 + 4 * osn(osnCount))
                        blocks(3) = Etbl(2, 1 + 4 * osn(osnCount))
                        blocks(4) = Etbl(4, 0 + 4 * osn(osnCount))
                        blocks(5) = Etbl(4, 1 + 4 * osn(osnCount))
                        blocks(6) = Etbl(5, 0 + 4 * osn(osnCount))
                        blocks(7) = Etbl(5, 1 + 4 * osn(osnCount))
                        E3grp(0) = "500"
                        E3grp(1) = "5000"
                    End If
                    E1grp(0) = "50"
                    E1grp(1) = "500"
                Else
                    If e1 < 2000 Then
                        If e3 < 500 Then
                            blocks(0) = Etbl(0, 1 + 4 * osn(osnCount))
                            blocks(1) = Etbl(0, 2 + 4 * osn(osnCount)) '6
                            blocks(2) = Etbl(1, 1 + 4 * osn(osnCount))
                            blocks(3) = Etbl(1, 2 + 4 * osn(osnCount))
                            blocks(4) = Etbl(3, 1 + 4 * osn(osnCount))
                            blocks(5) = Etbl(3, 2 + 4 * osn(osnCount))
                            blocks(6) = Etbl(4, 1 + 4 * osn(osnCount))
                            blocks(7) = Etbl(4, 2 + 4 * osn(osnCount))
                            E3grp(0) = "50"
                            E3grp(1) = "500"
                        Else
                            blocks(0) = Etbl(1, 1 + 4 * osn(osnCount))
                            blocks(1) = Etbl(1, 2 + 4 * osn(osnCount))
                            blocks(2) = Etbl(2, 1 + 4 * osn(osnCount))
                            blocks(3) = Etbl(2, 2 + 4 * osn(osnCount))
                            blocks(4) = Etbl(4, 1 + 4 * osn(osnCount))
                            blocks(5) = Etbl(4, 2 + 4 * osn(osnCount))
                            blocks(6) = Etbl(5, 1 + 4 * osn(osnCount))
                            blocks(7) = Etbl(5, 2 + 4 * osn(osnCount))
                            E3grp(0) = "500"
                            E3grp(1) = "5000"
                        End If
                        E1grp(0) = "500"
                        E1grp(1) = "2000"
                    Else
                        If e3 < 500 Then
                            blocks(0) = Etbl(0, 2 + 4 * osn(osnCount))
                            blocks(1) = Etbl(0, 3 + 4 * osn(osnCount))
                            blocks(2) = Etbl(1, 2 + 4 * osn(osnCount))
                            blocks(3) = Etbl(1, 3 + 4 * osn(osnCount))
                            blocks(4) = Etbl(3, 2 + 4 * osn(osnCount))
                            blocks(5) = Etbl(3, 3 + 4 * osn(osnCount))
                            blocks(6) = Etbl(4, 2 + 4 * osn(osnCount))
                            blocks(7) = Etbl(4, 3 + 4 * osn(osnCount))
                            E3grp(0) = "50"
                            E3grp(1) = "500"
                        Else
                            blocks(0) = Etbl(1, 2 + 4 * osn(osnCount)) '6
                            blocks(1) = Etbl(1, 3 + 4 * osn(osnCount))
                            blocks(2) = Etbl(2, 2 + 4 * osn(osnCount))
                            blocks(3) = Etbl(2, 3 + 4 * osn(osnCount))
                            blocks(4) = Etbl(4, 2 + 4 * osn(osnCount))
                            blocks(5) = Etbl(4, 3 + 4 * osn(osnCount))
                            blocks(6) = Etbl(5, 2 + 4 * osn(osnCount))
                            blocks(7) = Etbl(5, 3 + 4 * osn(osnCount))
                            E3grp(0) = "500"
                            E3grp(1) = "5000"
                        End If
                        E1grp(0) = "2000" '2000
                        E1grp(1) = "5000"
                    End If
                End If
                DEB = 450
                DEM = e2 - 50
            Else
                If e2 < 2000 Then
                    If e1 < 500 Then
                        If e3 < 500 Then

                            blocks(0) = Etbl(3, 0 + 4 * osn(osnCount))
                            blocks(1) = Etbl(3, 1 + 4 * osn(osnCount))
                            blocks(2) = Etbl(4, 0 + 4 * osn(osnCount))
                            blocks(3) = Etbl(4, 1 + 4 * osn(osnCount))
                            blocks(4) = Etbl(6, 0 + 4 * osn(osnCount))
                            blocks(5) = Etbl(6, 1 + 4 * osn(osnCount))
                            blocks(6) = Etbl(7, 0 + 4 * osn(osnCount))
                            blocks(7) = Etbl(7, 1 + 4 * osn(osnCount))
                            E3grp(0) = "50"
                            E3grp(1) = "500"
                        Else
                            blocks(0) = Etbl(4, 0 + 4 * osn(osnCount))
                            blocks(1) = Etbl(4, 1 + 4 * osn(osnCount))
                            blocks(2) = Etbl(5, 0 + 4 * osn(osnCount))
                            blocks(3) = Etbl(5, 1 + 4 * osn(osnCount))
                            blocks(4) = Etbl(7, 0 + 4 * osn(osnCount))
                            blocks(5) = Etbl(7, 1 + 4 * osn(osnCount))
                            blocks(6) = Etbl(8, 0 + 4 * osn(osnCount))
                            blocks(7) = Etbl(8, 1 + 4 * osn(osnCount))
                            E3grp(0) = "500"
                            E3grp(1) = "5000"
                        End If
                        E1grp(0) = "50"
                        E1grp(1) = "500"
                    Else
                        If e1 < 2000 Then
                            If e3 < 500 Then
                                blocks(0) = Etbl(3, 1 + 4 * osn(osnCount))
                                blocks(1) = Etbl(3, 2 + 4 * osn(osnCount)) '6
                                blocks(2) = Etbl(4, 1 + 4 * osn(osnCount))
                                blocks(3) = Etbl(4, 2 + 4 * osn(osnCount))
                                blocks(4) = Etbl(6, 1 + 4 * osn(osnCount))
                                blocks(5) = Etbl(6, 2 + 4 * osn(osnCount))
                                blocks(6) = Etbl(7, 1 + 4 * osn(osnCount))
                                blocks(7) = Etbl(7, 2 + 4 * osn(osnCount))
                                E3grp(0) = "50"
                                E3grp(1) = "500"
                            Else
                                blocks(0) = Etbl(4, 1 + 4 * osn(osnCount))
                                blocks(1) = Etbl(4, 2 + 4 * osn(osnCount))
                                blocks(2) = Etbl(5, 1 + 4 * osn(osnCount))
                                blocks(3) = Etbl(5, 2 + 4 * osn(osnCount))
                                blocks(4) = Etbl(7, 1 + 4 * osn(osnCount))
                                blocks(5) = Etbl(7, 2 + 4 * osn(osnCount))
                                blocks(6) = Etbl(8, 1 + 4 * osn(osnCount))
                                blocks(7) = Etbl(8, 2 + 4 * osn(osnCount))
                                E3grp(0) = "500"
                                E3grp(1) = "5000"
                            End If
                            E1grp(0) = "500"
                            E1grp(1) = "2000" '2000
                        Else
                            If e3 < 500 Then
                                blocks(0) = Etbl(3, 2 + 4 * osn(osnCount)) '6
                                blocks(1) = Etbl(3, 3 + 4 * osn(osnCount))
                                blocks(2) = Etbl(4, 2 + 4 * osn(osnCount))
                                blocks(3) = Etbl(4, 3 + 4 * osn(osnCount))
                                blocks(4) = Etbl(6, 2 + 4 * osn(osnCount))
                                blocks(5) = Etbl(6, 3 + 4 * osn(osnCount))
                                blocks(6) = Etbl(7, 2 + 4 * osn(osnCount))
                                blocks(7) = Etbl(7, 3 + 4 * osn(osnCount))
                                E3grp(0) = "50"
                                E3grp(1) = "500"
                            Else
                                blocks(0) = Etbl(4, 2 + 4 * osn(osnCount)) '6
                                blocks(1) = Etbl(4, 3 + 4 * osn(osnCount))
                                blocks(2) = Etbl(5, 2 + 4 * osn(osnCount))
                                blocks(3) = Etbl(5, 3 + 4 * osn(osnCount))
                                blocks(4) = Etbl(7, 2 + 4 * osn(osnCount))
                                blocks(5) = Etbl(7, 3 + 4 * osn(osnCount))
                                blocks(6) = Etbl(8, 2 + 4 * osn(osnCount))
                                blocks(7) = Etbl(8, 3 + 4 * osn(osnCount))
                                E3grp(0) = "500"
                                E3grp(1) = "5000"
                            End If
                            E1grp(0) = "2000" '2000
                            E1grp(1) = "5000"
                        End If
                    End If
                    DEB = 1500
                    DEM = e2 - 500
                Else
                    If e1 < 500 Then
                        If e3 < 500 Then

                            blocks(0) = Etbl(6, 0 + 4 * osn(osnCount))
                            blocks(1) = Etbl(6, 1 + 4 * osn(osnCount))
                            blocks(2) = Etbl(7, 0 + 4 * osn(osnCount))
                            blocks(3) = Etbl(7, 1 + 4 * osn(osnCount))
                            blocks(4) = Etbl(9, 0 + 4 * osn(osnCount))
                            blocks(5) = Etbl(9, 1 + 4 * osn(osnCount))
                            blocks(6) = Etbl(10, 0 + 4 * osn(osnCount))
                            blocks(7) = Etbl(10, 1 + 4 * osn(osnCount))
                            E3grp(0) = "50"
                            E3grp(1) = "500"
                        Else
                            blocks(0) = Etbl(7, 0 + 4 * osn(osnCount))
                            blocks(1) = Etbl(7, 1 + 4 * osn(osnCount))
                            blocks(2) = Etbl(8, 0 + 4 * osn(osnCount))
                            blocks(3) = Etbl(8, 1 + 4 * osn(osnCount))
                            blocks(4) = Etbl(10, 0 + 4 * osn(osnCount))
                            blocks(5) = Etbl(10, 1 + 4 * osn(osnCount))
                            blocks(6) = Etbl(11, 0 + 4 * osn(osnCount))
                            blocks(7) = Etbl(11, 1 + 4 * osn(osnCount))
                            E3grp(0) = "500"
                            E3grp(1) = "5000"
                        End If
                        E1grp(0) = "50"
                        E1grp(1) = "500"
                    Else
                        If e1 < 2000 Then
                            If e3 < 500 Then
                                blocks(0) = Etbl(6, 1 + 4 * osn(osnCount))
                                blocks(1) = Etbl(6, 2 + 4 * osn(osnCount)) '6
                                blocks(2) = Etbl(7, 1 + 4 * osn(osnCount))
                                blocks(3) = Etbl(7, 2 + 4 * osn(osnCount))
                                blocks(4) = Etbl(9, 1 + 4 * osn(osnCount))
                                blocks(5) = Etbl(9, 2 + 4 * osn(osnCount))
                                blocks(6) = Etbl(10, 1 + 4 * osn(osnCount))
                                blocks(7) = Etbl(10, 2 + 4 * osn(osnCount))
                                E3grp(0) = "50"
                                E3grp(1) = "500"
                            Else
                                blocks(0) = Etbl(7, 1 + 4 * osn(osnCount))
                                blocks(1) = Etbl(7, 2 + 4 * osn(osnCount))
                                blocks(2) = Etbl(8, 1 + 4 * osn(osnCount))
                                blocks(3) = Etbl(8, 2 + 4 * osn(osnCount))
                                blocks(4) = Etbl(10, 1 + 4 * osn(osnCount))
                                blocks(5) = Etbl(10, 2 + 4 * osn(osnCount))
                                blocks(6) = Etbl(11, 1 + 4 * osn(osnCount))
                                blocks(7) = Etbl(11, 2 + 4 * osn(osnCount))
                                E3grp(0) = "500"
                                E3grp(1) = "5000"
                            End If
                            E1grp(0) = "500"
                            E1grp(1) = "2000" '2000
                        Else
                            If e3 < 500 Then
                                blocks(0) = Etbl(6, 2 + 4 * osn(osnCount)) '6
                                blocks(1) = Etbl(6, 3 + 4 * osn(osnCount))
                                blocks(2) = Etbl(7, 2 + 4 * osn(osnCount))
                                blocks(3) = Etbl(7, 3 + 4 * osn(osnCount))
                                blocks(4) = Etbl(9, 2 + 4 * osn(osnCount))
                                blocks(5) = Etbl(9, 3 + 4 * osn(osnCount))
                                blocks(6) = Etbl(10, 2 + 4 * osn(osnCount))
                                blocks(7) = Etbl(10, 3 + 4 * osn(osnCount))
                                E3grp(0) = "50"
                                E3grp(1) = "500"
                            Else
                                blocks(0) = Etbl(7, 2 + 4 * osn(osnCount)) '6
                                blocks(1) = Etbl(7, 3 + 4 * osn(osnCount))
                                blocks(2) = Etbl(8, 2 + 4 * osn(osnCount))
                                blocks(3) = Etbl(8, 3 + 4 * osn(osnCount))
                                blocks(4) = Etbl(10, 2 + 4 * osn(osnCount))
                                blocks(5) = Etbl(10, 3 + 4 * osn(osnCount))
                                blocks(6) = Etbl(11, 2 + 4 * osn(osnCount))
                                blocks(7) = Etbl(11, 3 + 4 * osn(osnCount))
                                E3grp(0) = "500"
                                E3grp(1) = "5000"
                            End If
                            E1grp(0) = "2000"
                            E1grp(1) = "5000"
                        End If
                    End If
                    DEB = 3000
                    DEM = e2 - 2000
                End If
            End If


            'для слоев
            For sloIcount = 1 To 4
                For blockcount = 0 To 7
                    'определение подблоков по толщине h2
                    If h2 < 5 Then
                        H2grp(0) = 1
                        H2grp(1) = 2
                        DHB = 4
                        DHM = h2 - 1
                    Else
                        H2grp(0) = 2
                        H2grp(1) = 3
                        DHB = 5
                        DHM = h2 - 5
                    End If
                    'все напряжения
                    For streSScount = 1 To 9
                        Select Case streSScount
                            Case 1
                                SSl = "R" '- rast
                            Case 2
                                SSl = "S" '- sjim
                            Case 3
                                SSl = "d" '
                            Case 4
                                SSl = "Sx"
                            Case 5
                                SSl = "Sy"
                            Case 6
                                SSl = "Sz"
                            Case 7
                                SSl = "Sxy"
                            Case 8
                                SSl = "Syz"
                            Case 9
                                SSl = "Szx"
                        End Select
                        'интерполяция напряжений по толщинам 1 и 3 для двух толщин 2
                        For h2Count = 0 To 1
                            nomopath = basePath+"\" & SSl & "-" & sloIcount & "-" & blocks(blockcount) & "-" & H2grp(h2Count) & "-150.ndt"

                            'TextBox8.Text = TextBox8.Text & nomopath & vbCrLf

                            MN.TextToArray(nomopath, Nomo)
                            R(h2Count) = MN.GetValue(h3, h1, Nomo)

                        Next h2Count
                        'интерполяция по толщине 2
                        blockReZ(blockcount, streSScount) = R(0) + ((R(1) - R(0)) / DHB) * DHM
                    Next streSScount


                Next blockcount
                'создание временных номограмм для интерполяции по модулям 1 и 3
                For streSScount = 1 To 9
                    For h2Count = 0 To 1
                        If h2Count = 0 Then
                            TempNomogen(blockReZ(0, streSScount), blockReZ(1, streSScount), blockReZ(2, streSScount), blockReZ(3, streSScount), CStr(h2Count) & CStr(streSScount) & CStr(sloIcount))
                        Else
                            TempNomogen(blockReZ(4, streSScount), blockReZ(5, streSScount), blockReZ(6, streSScount), blockReZ(7, streSScount), CStr(h2Count) & CStr(streSScount) & CStr(sloIcount))
                        End If
                        nomopath = basePath+"\temp" & CStr(h2Count) & CStr(streSScount) & CStr(sloIcount) & ".ndt"
                        MN.TextToArray(nomopath, Nomo)
                        R(h2Count) = MN.GetValue(e3, e1, Nomo)
                    Next h2Count
                    'интерполяция по e2
                    Rez(osnCount, sloIcount, streSScount) = Math.Round(R(0) + ((R(1) - R(0)) / DEB) * DEM, 3)
                Next streSScount


            Next sloIcount
        Next osnCount
        nomopath = ""
        For sloIcount = 1 To 4
            For streSScount = 1 To 9
                RezF(streSScount) = Math.Round(Rez(0, sloIcount, streSScount) + ((Rez(1, sloIcount, streSScount) - Rez(0, sloIcount, streSScount)) / DOB) * DOM, 3)

            Next streSScount
            rezTS = TS(RezF(4) / 1000, RezF(5) / 1000, RezF(6) / 1000, RezF(7) / 1000, RezF(8) / 1000, RezF(9) / 1000)
            
            Szim(sloIcount) = CStr(RezF(2))
            mTau(sloIcount) = CStr(AnglesF(3))
            mSigma(sloIcount) = CStr(AnglesF(4))
                

        Next sloIcount

        model.Szim1=Szim(1)
        model.Szim2=Szim(2)
        model.Szim3=Szim(3)

        model.Sigma1 = mSigma(1)
        model.Sigma2 = mSigma(2)
        model.Sigma3 = mSigma(3)

        model.Tau1 = mTau(1)
        model.Tau2 = mTau(2)
        model.Tau3 = mTau(3)

        Calculate = model

    End Function
    
    Private Sub TempNomogen(ByVal val1 As Single, ByVal val2 As Single, ByVal val3 As Single, ByVal val4 As Single, ByVal prefix As String)
        Dim fi As New System.IO.FileInfo(basePath+"\temp" & prefix & ".ndt")
        If fi.Exists Then fi.Delete()
        SW = fi.CreateText
        strdata = "2" & vbTab & "2"
        SW.WriteLine(strdata)
        strdata = "0" & vbTab & E1grp(0) & vbTab & E1grp(1)
        SW.WriteLine(strdata)
        strdata = E3grp(0) & vbTab & CStr(val1) & vbTab & CStr(val2)
        SW.WriteLine(strdata)
        strdata = E3grp(1) & vbTab & CStr(val3) & vbTab & CStr(val4)
        SW.WriteLine(strdata)
        SW.Close()
    End Sub
    
    Private Function TS(ByVal Sx As Single, ByVal Sy As Single, ByVal Sz As Single, ByVal Txy As Single, ByVal Tyz As Single, ByVal Tzx As Single) As Single
        Dim j As Integer
        Dim MTR As Double
        'TS = 0
        TR = 0
        MTR = -999999
        For j = 1 To AngCount - 1
            'px
            Angles(j, 4) = Sx * Angles(j, 1) + Txy * Angles(j, 2) + Tzx * Angles(j, 3)
            'py
            Angles(j, 5) = Txy * Angles(j, 1) + Sy * Angles(j, 2) + Tyz * Angles(j, 3)
            'pz
            Angles(j, 6) = Tzx * Angles(j, 1) + Tyz * Angles(j, 2) + Sz * Angles(j, 3)
            's()
            Angles(j, 7) = Angles(j, 4) * Angles(j, 1) + Angles(j, 5) * Angles(j, 2) + Angles(j, 6) * Angles(j, 3)
            'p'()
            Angles(j, 8) = Math.Sqrt(Angles(j, 4) ^ 2 + Angles(j, 5) ^ 2 + Angles(j, 6) ^ 2)
            't()
            Angles(j, 9) = Math.Sqrt(Angles(j, 8) ^ 2 - Angles(j, 7) ^ 2)
            '
            Angles(j, 10) = Angles(j, 9) - Math.Abs(Angles(j, 7))
            If Angles(j, 10) > 0 Then
                If Angles(j, 10) > TR Then
                    TR = Angles(j, 10)
                    AnglesF(0) = Math.Round(Math.Acos(Angles(j, 1)) * 180 / Math.PI, 0)
                    AnglesF(1) = Math.Round(Math.Acos(Angles(j, 2)) * 180 / Math.PI, 0)
                    AnglesF(2) = Math.Round(Math.Acos(Angles(j, 3)) * 180 / Math.PI, 0)
                    AnglesF(3) = Math.Round(Angles(j, 9), 3)
                    AnglesF(4) = Math.Round(Angles(j, 7), 3)
                End If
            Else
                If Angles(j, 10) > MTR Then
                    MTR = Angles(j, 10)
                    AnglesF(5) = Math.Round(Math.Acos(Angles(j, 1)) * 180 / Math.PI, 0)
                    AnglesF(6) = Math.Round(Math.Acos(Angles(j, 2)) * 180 / Math.PI, 0)
                    AnglesF(7) = Math.Round(Math.Acos(Angles(j, 3)) * 180 / Math.PI, 0)
                    AnglesF(8) = Math.Round(Angles(j, 9), 3)
                    AnglesF(9) = Math.Round(Angles(j, 7), 3)
                End If
            End If

            'If Angles(j, 9) > TR Then TR = Angles(j, 9)
        Next
        If TR = 0 Then
            TS = MTR
            AnglesF(0) = AnglesF(5)
            AnglesF(1) = AnglesF(6)
            AnglesF(2) = AnglesF(7)
            AnglesF(3) = AnglesF(8)
            AnglesF(4) = AnglesF(9)
        Else
            TS = TR
        End If
    End Function
    
End Class
