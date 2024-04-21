' MIT License
' 
' Copyright (c) 2024 skt001
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.

Imports System.Text

Public MustInherit Class RomHeader
    Public Property Prefix As String
    Public Property Extensions As String()
    Public Property HeaderSize As Integer
    Public Property HeaderBytes As Byte()

    Public Sub New()
        Prefix = ""
        Extensions = New String() {}
        HeaderSize = 0
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Shared Function GetPrefix(romHeaderType As Type) As String
        Select Case romHeaderType
            Case GetType(NesRomHeader)
                Return "NES_"
            Case GetType(SnesRomHeader)
                Return "SNES_"
            Case GetType(N64RomHeader)
                Return "N64_"
            Case GetType(GbRomHeader)
                Return "GB_"
            Case GetType(GbaRomHeader)
                Return "GBA_"
            Case GetType(NdsRomHeader)
                Return "NDS_"
            Case GetType(PceHeader)
                Return "PCE_"
            Case GetType(MegaDriveHeader)
                Return "MD_"
            Case GetType(DsiRomHeader)
                Return "DSi_"
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function GetExtensions(romHeaderType As Type) As String()
        Select Case romHeaderType
            Case GetType(NesRomHeader)
                Return {".nes"}
            Case GetType(SnesRomHeader)
                Return {".smc", ".sfc"}
            Case GetType(N64RomHeader)
                Return {".n64", ".v64", ".z64"}
            Case GetType(GbRomHeader)
                Return {".gb", ".gbc"}
            Case GetType(GbaRomHeader)
                Return {".gba"}
            Case GetType(NdsRomHeader)
                Return {".nds"}
            Case GetType(PceHeader)
                Return {".pce"}
            Case GetType(MegaDriveHeader)
                Return {".md", ".bin"}
            Case GetType(DsiRomHeader)
                Return {".dsi"}
            Case Else
                Return {}
        End Select
    End Function

    Public Shared Function GetSupportedHeaderTypes() As Type()
        Return {GetType(NesRomHeader),
            GetType(SnesRomHeader),
            GetType(N64RomHeader),
            GetType(GbRomHeader),
            GetType(GbaRomHeader),
            GetType(NdsRomHeader),
            GetType(DsiRomHeader),
            GetType(PceHeader),
            GetType(MegaDriveHeader)
            }
    End Function

    Public Shared Function GetRomHeaderFromExtension(extension As String) As RomHeader
        Select Case extension
            Case ".nes"
                Return New NesRomHeader()
            Case ".smc", ".sfc"
                Return New SnesRomHeader()
            Case ".n64", ".v64", ".z64"
                Return New N64RomHeader()
            Case ".gb", ".gbc"
                Return New GbRomHeader()
            Case ".gba"
                Return New GbaRomHeader()
            Case ".nds"
                Return New NdsRomHeader()
            Case ".pce"
                Return New PceHeader()
            Case ".md", ".bin"
                Return New MegaDriveHeader()
            Case ".dsi"
                Return New DsiRomHeader()
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function GetHeaderInfo() As String
        Return BitConverter.ToString(HeaderBytes, 0, HeaderSize).Replace("-", "")
    End Function

    Public Function GetMappedValue(translation As Dictionary(Of String, String), key As String) As String
        If translation.ContainsKey(key) Then
            Return translation(key)
        Else
            Return $"Unknown ({key})"
        End If
    End Function

    Public Function GetMappedCharValue(translation As Dictionary(Of Char, String), key As Char) As String
        If translation.ContainsKey(key) Then
            Return translation(key)
        Else
            Return $"Unknown ({key})"
        End If
    End Function

    Public MustOverride Sub SetHeaderInfo()
End Class

Public Class NesRomHeader
    Inherits RomHeader
    Public Sub New()
        Prefix = "NES_"
        Extensions = New String() {".nes"}
        HeaderSize = 16
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property MagicNumber As String
    Public Property PrgRomSize As String
    Public Property ChrRomSize As String
    Public Property Mapper As String
    Public Property VerticalMirroring As String
    Public Property BatteryBacked As String
    Public Property Trainer As String
    Public Property FourScreenMode As String
    Public Property VsUnisystem As String
    Public Property PlayChoice10 As String
    Public Property PrgRamSize As String
    Public Property TVSystem As String
    Public Property PrgRamPresent As String
    Public Property BusConflicts As String

    Private Shared ReadOnly NesMapperTypes As New Dictionary(Of String, String) From {
        {"0", "NROM"},
        {"1", "MMC1"},
        {"2", "UNROM"},
        {"3", "CNROM"},
        {"4", "MMC3"},
        {"5", "MMC5"},
        {"6", "FFE F4xxx"},
        {"7", "AOROM"},
        {"8", "FFE F3xxx"},
        {"9", "MMC2"},
        {"10", "MMC4"},
        {"11", "Color Dreams"},
        {"12", "FFE F6xxx"},
        {"13", "CPROM"},
        {"15", "100-in-1 Contra Function 16"},
        {"16", "Bandai"},
        {"17", "FFE F8xxx"},
        {"18", "Jaleco SS8806"},
        {"19", "Namco 163"},
        {"20", "Famicom Disk System"},
        {"21", "Konami VRC4 2A"},
        {"22", "Konami VRC2A"},
        {"23", "Konami VRC2B"},
        {"24", "Konami VRC6 A"},
        {"25", "Konami VRC4 1"},
        {"32", "IREM G-101"},
        {"33", "Taito TC0190"},
        {"34", "Nina 1"},
        {"64", "Tengen RAMBO 1"},
        {"65", "Irem H3001"},
        {"66", "GxROM"},
        {"67", "Sunsoft 3"},
        {"68", "Sunsoft 4"},
        {"69", "Sunsoft FME-7"}
    }

    Private Shared ReadOnly NesMirroringTypes As New Dictionary(Of String, String) From {
        {"0", "Horizontal"},
        {"1", "Vertical"}
    }

    Private Shared ReadOnly NesBatteryTypes As New Dictionary(Of String, String) From {
        {"0", "No Battery"},
        {"1", "Battery Backed"}
    }

    Private Shared ReadOnly NesTrainerTypes As New Dictionary(Of String, String) From {
        {"0", "No Trainer"},
        {"1", "512-byte Trainer"}
    }

    Private Shared ReadOnly NesFourScreenTypes As New Dictionary(Of String, String) From {
        {"0", "No Four Screen"},
        {"1", "Four Screen"}
    }

    Private Shared ReadOnly NesVSTypes As New Dictionary(Of String, String) From {
        {"0", "Regular Cartridge"},
        {"1", "VS-Unisystem"}
    }

    Private Shared ReadOnly NesPlaychoiceTypes As New Dictionary(Of String, String) From {
        {"0", "Regular Cartridge"},
        {"1", "PlayChoice-10"}
    }

    Private Shared ReadOnly NesTvSystemTypes As New Dictionary(Of String, String) From {
        {"0", "NTSC"},
        {"1", "PAL"},
        {"2", "NTSC and PAL"},
        {"3", "NTSC and PAL"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 16 Then
            MagicNumber = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 4)
            PrgRomSize = HeaderBytes(4).ToString()
            ChrRomSize = HeaderBytes(5).ToString()

            Dim mapperLow As Integer = HeaderBytes(6) >> 4
            Dim mapperHigh As Integer = HeaderBytes(7) And &HF0
            Mapper = (mapperHigh << 4 Or mapperLow).ToString()
            VerticalMirroring = GetMappedValue(NesMirroringTypes, (HeaderBytes(6) And &H1).ToString())
            BatteryBacked = GetMappedValue(NesBatteryTypes, ((HeaderBytes(6) And &H2) >> 1).ToString())
            Trainer = GetMappedValue(NesTrainerTypes, ((HeaderBytes(6) And &H4) >> 2).ToString())
            FourScreenMode = GetMappedValue(NesFourScreenTypes, ((HeaderBytes(6) And &H8) >> 3).ToString())
            VsUnisystem = GetMappedValue(NesVSTypes, (HeaderBytes(7) And &H1).ToString())
            PlayChoice10 = GetMappedValue(NesPlaychoiceTypes, ((HeaderBytes(7) And &H2) >> 1).ToString())
            PrgRamSize = HeaderBytes(8).ToString()
            TVSystem = GetMappedValue(NesTvSystemTypes, (HeaderBytes(9) And &H1).ToString())
            PrgRamPresent = ((HeaderBytes(10) And &H10) >> 4).ToString()
            BusConflicts = ((HeaderBytes(10) And &H20) >> 5).ToString()
        Else
            MagicNumber = ""
            PrgRomSize = ""
            ChrRomSize = ""
            Mapper = ""
            VerticalMirroring = ""
            BatteryBacked = ""
            Trainer = ""
            FourScreenMode = ""
            VsUnisystem = ""
            PlayChoice10 = ""
            PrgRamSize = ""
            TVSystem = ""
            PrgRamPresent = ""
            BusConflicts = ""
        End If
    End Sub
End Class

Public Class SnesRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "SNES_"
        Extensions = New String() {".smc", ".sfc", ".fig", ".swc"}
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property PageCount As String
    Public Property EmulationMode As String
    Public Property Reserved1 As String
    Public Property FileIdCode1 As String
    Public Property FileIdCode2 As String
    Public Property FileType As String
    Public Property Reserved2 As String

    Private Shared ReadOnly FileTypes As New Dictionary(Of String, String) From {
        {"02", "Magic Griffin Game File (PC Engine)"},
        {"03", "Magic Griffin SRAM Data File"},
        {"04", "SWC & SSM Game File (Super Magicom)"},
        {"05", "SWC & SMC Password, SRAM Data, Saver Data File"},
        {"06", "SMD Game File (Mega Drive)"},
        {"07", "SMD Sram Data File"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 512 Then
            PageCount = BitConverter.ToUInt16(HeaderBytes, 0).ToString()

            Dim emulationModeByte As Byte = HeaderBytes(2)
            Dim emulationModeFlags As New List(Of String)()

            If (emulationModeByte And 128) <> 0 Then emulationModeFlags.Add("Run in Mode 0 (Jump $8000)")
            If (emulationModeByte And 64) = 0 Then emulationModeFlags.Add("Last File of the Game (Multi File Loading)")
            If (emulationModeByte And 32) <> 0 Then emulationModeFlags.Add("Mode 2 (SRAM Mapping)") Else emulationModeFlags.Add("Mode 1 (SRAM Mapping)")
            If (emulationModeByte And 16) <> 0 Then emulationModeFlags.Add("Mode 21 (DRAM Mapping)") Else emulationModeFlags.Add("Mode 20 (DRAM Mapping)")

            Dim sramSizeBits As Integer = (emulationModeByte And 12) >> 2
            Select Case sramSizeBits
                Case 0
                    emulationModeFlags.Add("SRAM Off")
                Case 1
                    emulationModeFlags.Add("SRAM 16K")
                Case 2
                    emulationModeFlags.Add("SRAM 64K")
                Case 3
                    emulationModeFlags.Add("SRAM 256K")
            End Select

            If (emulationModeByte And 2) <> 0 Then emulationModeFlags.Add("Run in Mode 2 (JMP Reset)") Else emulationModeFlags.Add("Run in Mode 3 (JMP Reset)")
            If (emulationModeByte And 1) <> 0 Then emulationModeFlags.Add("Enable (External Cartridge Memory)") Else emulationModeFlags.Add("Disable (External Cartridge Memory)")

            EmulationMode = String.Join(", ", emulationModeFlags)

            Reserved1 = BitConverter.ToString(HeaderBytes, 3, 5).Replace("-", " ")

            FileIdCode1 = System.Text.Encoding.ASCII.GetString(HeaderBytes, 8, 1)
            FileIdCode2 = System.Text.Encoding.ASCII.GetString(HeaderBytes, 9, 1)

            If FileIdCode1 = "A" AndAlso FileIdCode2 = "B" Then
                FileType = GetMappedValue(FileTypes, HeaderBytes(10).ToString())
            Else
                FileType = "Unknown"
            End If

            Reserved2 = BitConverter.ToString(HeaderBytes, 11, 501).Replace("-", " ")
        Else
            ' ヘッダーが不完全な場合の処理
            PageCount = ""
            EmulationMode = ""
            Reserved1 = ""
            FileIdCode1 = ""
            FileIdCode2 = ""
            FileType = ""
            Reserved2 = ""
        End If
    End Sub
End Class

Public Class N64RomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "N64_"
        Extensions = New String() {".z64", ".v64", ".n64"}
        HeaderSize = 64
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' N64固有のプロパティ
    Public Property Endianness As String
    Public Property InitialPIBSBDOM1LATREG As String
    Public Property InitialPIBSDDOM1PGSREG1 As String
    Public Property InitialPIBSDDOM1PWDREG As String
    Public Property InitialPIBSBDOM1PGSREG2 As String
    Public Property ClockRate As String
    Public Property ProgramCounter As String
    Public Property ReleaseAddress As String
    Public Property CRC1 As String
    Public Property CRC2 As String
    Public Property Unknown1 As String ' 0x18-0x1Fのバイト（不明/未使用）
    Public Property ImageName As String
    Public Property Unknown2 As String ' 0x34-0x37のバイト（不明/未使用）
    Public Property MediaFormat As String
    Public Property CartridgeID As String
    Public Property CountryCode As String
    Public Property Version As String
    Public Property BootCode As String

    ' 国コードの変換テーブル
    Private Shared ReadOnly CountryCodes As New Dictionary(Of String, String) From {
        {"37", "Beta"},
        {"41", "Asian (NTSC)"},
        {"42", "Brazilian"},
        {"43", "Chinese"},
        {"44", "German"},
        {"45", "North America"},
        {"46", "French"},
        {"47", "Gateway 64 (NTSC)"},
        {"48", "Dutch"},
        {"49", "Italian"},
        {"4A", "Japanese"},
        {"4B", "Korean"},
        {"4C", "Gateway 64 (PAL)"},
        {"4E", "Canadian"},
        {"50", "European (basic spec.)"},
        {"53", "Spanish"},
        {"55", "Australian"},
        {"57", "Scandinavian"},
        {"58", "European"},
        {"59", "European"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 64 Then
            Endianness = HeaderBytes(0).ToString()
            InitialPIBSBDOM1LATREG = (HeaderBytes(1) And 240).ToString()
            InitialPIBSDDOM1PGSREG1 = (HeaderBytes(1) And 15).ToString()
            InitialPIBSDDOM1PWDREG = HeaderBytes(2).ToString()
            InitialPIBSBDOM1PGSREG2 = HeaderBytes(3).ToString()
            ClockRate = BitConverter.ToUInt32(HeaderBytes, 4).ToString()
            ProgramCounter = BitConverter.ToUInt32(HeaderBytes, 8).ToString()
            ReleaseAddress = BitConverter.ToUInt32(HeaderBytes, 12).ToString()
            CRC1 = BitConverter.ToUInt32(HeaderBytes, 16).ToString()
            CRC2 = BitConverter.ToUInt32(HeaderBytes, 20).ToString()
            Unknown1 = BitConverter.ToString(HeaderBytes, 24, 8).Replace("-", "")
            ImageName = System.Text.Encoding.ASCII.GetString(HeaderBytes, 32, 20).TrimEnd(ControlChars.NullChar)
            Unknown2 = BitConverter.ToString(HeaderBytes, 52, 4).Replace("-", "")
            MediaFormat = System.Text.Encoding.ASCII.GetString(HeaderBytes, 56, 4)
            CartridgeID = System.Text.Encoding.ASCII.GetString(HeaderBytes, 60, 2)
            CountryCode = GetMappedValue(CountryCodes, HeaderBytes(62).ToString())
            Version = HeaderBytes(63).ToString()
            BootCode = BitConverter.ToString(HeaderBytes, 64, 4032).Replace("-", "")
        Else
            ' ヘッダーが不完全な場合の処理
            Endianness = ""
            InitialPIBSBDOM1LATREG = ""
            InitialPIBSDDOM1PGSREG1 = ""
            InitialPIBSDDOM1PWDREG = ""
            InitialPIBSBDOM1PGSREG2 = ""
            ClockRate = ""
            ProgramCounter = ""
            ReleaseAddress = ""
            CRC1 = ""
            CRC2 = ""
            Unknown1 = ""
            ImageName = ""
            Unknown2 = ""
            MediaFormat = ""
            CartridgeID = ""
            CountryCode = ""
            Version = ""
            BootCode = ""
        End If
    End Sub
End Class

Public Class GbRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "GB_"
        Extensions = New String() {".gb", ".gbc"}
        HeaderSize = 80
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property EntryPoint As String
    Public Property NintendoLogo As String
    Public Property Title As String
    Public Property ManufacturerCode As String
    Public Property CGBFlag As String
    Public Property NewLicenseeCode As String
    Public Property SGBFlag As String
    Public Property CartridgeType As String
    Public Property ROMSize As String
    Public Property RAMSize As String
    Public Property DestinationCode As String
    Public Property OldLicenseeCode As String
    Public Property MaskROMVersionNumber As String
    Public Property HeaderChecksum As String
    Public Property GlobalChecksum As String

    Private Shared ReadOnly MapperTypes As New Dictionary(Of String, String) From {
        {"00", "ROM ONLY"},
        {"01", "MBC1"},
        {"02", "MBC1+RAM"},
        {"03", "MBC1+RAM+BATTERY"},
        {"05", "MBC2"},
        {"06", "MBC2+BATTERY"},
        {"08", "ROM+RAM"},
        {"09", "ROM+RAM+BATTERY"},
        {"0B", "MMM01"},
        {"0C", "MMM01+RAM"},
        {"0D", "MMM01+RAM+BATTERY"},
        {"0F", "MBC3+TIMER+BATTERY"},
        {"10", "MBC3+TIMER+RAM+BATTERY"},
        {"11", "MBC3"},
        {"12", "MBC3+RAM"},
        {"13", "MBC3+RAM+BATTERY"},
        {"19", "MBC5"},
        {"1A", "MBC5+RAM"},
        {"1B", "MBC5+RAM+BATTERY"},
        {"1C", "MBC5+RUMBLE"},
        {"1D", "MBC5+RUMBLE+RAM"},
        {"1E", "MBC5+RUMBLE+RAM+BATTERY"},
        {"20", "MBC6"},
        {"22", "MBC7+SENSOR+RUMBLE+RAM+BATTERY"},
        {"FC", "POCKET CAMERA"},
        {"FD", "BANDAI TAMA5"},
        {"FE", "HuC3"},
        {"FF", "HuC1+RAM+BATTERY"}
    }

    Private Shared ReadOnly RamSizes As New Dictionary(Of String, String) From {
        {"00", "No RAM"},
        {"02", "8 KiB"},
        {"03", "32 KiB"},
        {"04", "128 KiB"},
        {"05", "64 KiB"}
    }

    Private Shared ReadOnly DestinationCodes As New Dictionary(Of String, String) From {
        {"00", "Japan"},
        {"01", "Non-Japan"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 80 Then
            EntryPoint = BitConverter.ToString(HeaderBytes, 0, 4).Replace("-", " ")
            NintendoLogo = BitConverter.ToString(HeaderBytes, 4, 48).Replace("-", " ")
            Title = System.Text.Encoding.ASCII.GetString(HeaderBytes, 52, 16).Trim()
            ManufacturerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 63, 4).Trim()
            CGBFlag = HeaderBytes(67).ToString()
            NewLicenseeCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 68, 2)
            SGBFlag = HeaderBytes(70).ToString()
            CartridgeType = GetMappedValue(MapperTypes, HeaderBytes(71).ToString())

            Dim romSizeValue As Integer = HeaderBytes(72)
            Select Case romSizeValue
                Case 0 To 8
                    Dim romSizeKB As Integer = 32 << romSizeValue
                    ROMSize = romSizeKB.ToString() + " KiB"
                Case 82
                    ROMSize = "1.1 MiB"
                Case 83
                    ROMSize = "1.2 MiB"
                Case 84
                    ROMSize = "1.5 MiB"
                Case Else
                    ROMSize = "Unknown"
            End Select

            RAMSize = GetMappedValue(RamSizes, HeaderBytes(73).ToString())
            DestinationCode = GetMappedValue(DestinationCodes, HeaderBytes(74).ToString())
            OldLicenseeCode = HeaderBytes(75).ToString()
            MaskROMVersionNumber = HeaderBytes(76).ToString()
            HeaderChecksum = HeaderBytes(77).ToString()
            GlobalChecksum = BitConverter.ToUInt16(HeaderBytes, 78).ToString()
        Else
            EntryPoint = ""
            NintendoLogo = ""
            Title = ""
            ManufacturerCode = ""
            CGBFlag = ""
            NewLicenseeCode = ""
            SGBFlag = ""
            CartridgeType = ""
            ROMSize = ""
            RAMSize = ""
            DestinationCode = ""
            OldLicenseeCode = ""
            MaskROMVersionNumber = ""
            HeaderChecksum = ""
            GlobalChecksum = ""
        End If
    End Sub
End Class

Public Class GbaRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "GBA_"
        Extensions = New String() {".gba"}
        HeaderSize = 256
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property EntryPoint As String
    Public Property NintendoLogo As String
    Public Property GameTitle As String
    Public Property GameCode As String
    Public Property GameCode1 As String
    Public Property GameCode23 As String
    Public Property GameCode4 As String
    Public Property MakerCode As String
    Public Property FixedValue As String
    Public Property MainUnitCode As String
    Public Property DeviceType As String
    Public Property Reserved1 As String
    Public Property SoftwareVersion As String
    Public Property ComplementCheck As String
    Public Property Reserved2 As String

    Public Property IsMultiboot As String
    Public Property MultibootEntryPoint As String
    Public Property MultibootRamAddress As String
    Public Property MultibootModeNumber As String
    Public Property MultibootSlaveID As String

    Private Shared ReadOnly GameCodeFirstCharMap As New Dictionary(Of Char, String) From {
        {"A"c, "Normal game; Older titles (mainly 2001..2003)"},
        {"B"c, "Normal game; Newer titles (2003..)"},
        {"C"c, "Normal game; Not used yet, but might be used for even newer titles"},
        {"F"c, "Famicom/Classic NES Series (software emulated NES games)"},
        {"K"c, "Yoshi and Koro Koro Puzzle (acceleration sensor)"},
        {"P"c, "e-Reader (dot-code scanner) (or NDS PassMe image when gamecode='PASS')"},
        {"R"c, "Warioware Twisted (cartridge with rumble and z-axis gyro sensor)"},
        {"U"c, "Boktai 1 and 2 (cartridge with RTC and solar sensor)"},
        {"V"c, "Drill Dozer (cartridge with rumble)"}
    }

    Private Shared ReadOnly GameCodeLastCharMap As New Dictionary(Of Char, String) From {
        {"J"c, "Japan"},
        {"E"c, "USA/English"},
        {"P"c, "Europe/Elsewhere"},
        {"D"c, "German"},
        {"F"c, "French"},
        {"I"c, "Italian"},
        {"S"c, "Spanish"}
    }

    Private Shared ReadOnly DebugFlagMap As New Dictionary(Of Byte, String) From {
        {&HA5, "FIQ/Undefined Instruction handler in the BIOS becomes unlocked"}
    }

    Private Shared ReadOnly DeviceTypeMap As New Dictionary(Of String, String) From {
        {"00", "9FFC000h/8MBIT DACS"},
        {"80", "9FE2000h/1MBIT DACS"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 192 Then
            EntryPoint = BitConverter.ToString(HeaderBytes, 0, 4).Replace("-", " ")
            NintendoLogo = BitConverter.ToString(HeaderBytes, 4, 156).Replace("-", " ")
            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 160, 12).Trim()
            GameCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 172, 4)
            MakerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 176, 2)
            FixedValue = HeaderBytes(178).ToString()
            MainUnitCode = HeaderBytes(179).ToString()
            DeviceType = GetMappedValue(DeviceTypeMap, HeaderBytes(180).ToString())
            Reserved1 = BitConverter.ToString(HeaderBytes, 181, 7).Replace("-", " ")
            SoftwareVersion = HeaderBytes(188).ToString()
            ComplementCheck = HeaderBytes(189).ToString()
            Reserved2 = BitConverter.ToString(HeaderBytes, 190, 2).Replace("-", " ")

            If GameCode.Length = 4 Then
                Dim firstChar As Char = GameCode(0)
                Dim lastChar As Char = GameCode(3)
                GameCode1 = GetMappedCharValue(GameCodeFirstCharMap, firstChar)
                GameCode23 = GameCode.Substring(1, 2)
                GameCode4 = GetMappedCharValue(GameCodeLastCharMap, lastChar)
            Else
                GameCode1 = ""
                GameCode23 = ""
                GameCode4 = ""
            End If

            Dim debugFlag As Byte = HeaderBytes(156)
            If DebugFlagMap.ContainsKey(debugFlag) Then
                NintendoLogo &= " (" & DebugFlagMap(debugFlag) & ")"
            End If

            If HeaderBytes.Length >= 256 AndAlso BitConverter.ToUInt32(HeaderBytes, 192) <> 0 Then
                IsMultiboot = "True"
                MultibootEntryPoint = BitConverter.ToString(HeaderBytes, 192, 4).Replace("-", " ")
                MultibootRamAddress = BitConverter.ToString(HeaderBytes, 196, 4).Replace("-", " ")
                MultibootModeNumber = HeaderBytes(200).ToString()
                MultibootSlaveID = HeaderBytes(201).ToString()
            Else
                IsMultiboot = "False"
                MultibootEntryPoint = ""
                MultibootRamAddress = ""
                MultibootModeNumber = ""
                MultibootSlaveID = ""
            End If
        Else
            EntryPoint = ""
            NintendoLogo = ""
            GameTitle = ""
            GameCode = ""
            GameCode1 = ""
            GameCode23 = ""
            GameCode4 = ""
            MakerCode = ""
            FixedValue = ""
            MainUnitCode = ""
            DeviceType = ""
            Reserved1 = ""
            SoftwareVersion = ""
            ComplementCheck = ""
            Reserved2 = ""
            IsMultiboot = False
            MultibootEntryPoint = ""
            MultibootRamAddress = ""
            MultibootModeNumber = ""
            MultibootSlaveID = ""
        End If
    End Sub
End Class

Public Class NdsRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "NDS_"
        Extensions = New String() {".nds"}
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property GameTitle As String
    Public Property GameCode As String
    Public Property MakerCode As String
    Public Property UnitCode As String
    Public Property EncryptionSeedSelect As String
    Public Property DeviceCapacity As String
    Public Property Reserved1 As String
    Public Property Reserved2 As String
    Public Property NdsRegion As String
    Public Property RomVersion As String
    Public Property Autostart As String
    Public Property Arm9RomOffset As String
    Public Property Arm9EntryAddress As String
    Public Property Arm9RamAddress As String
    Public Property Arm9Size As String
    Public Property Arm7RomOffset As String
    Public Property Arm7EntryAddress As String
    Public Property Arm7RamAddress As String
    Public Property Arm7Size As String
    Public Property FntOffset As String
    Public Property FntSize As String
    Public Property FatOffset As String
    Public Property FatSize As String
    Public Property Arm9OverlayOffset As String
    Public Property Arm9OverlaySize As String
    Public Property Arm7OverlayOffset As String
    Public Property Arm7OverlaySize As String
    Public Property NormalCommandSetting As String
    Public Property Key1CommandSetting As String
    Public Property IconTitleOffset As String
    Public Property SecureAreaChecksum As String
    Public Property SecureAreaDelay As String
    Public Property Arm9AutoLoadAddress As String
    Public Property Arm7AutoLoadAddress As String
    Public Property SecureAreaDisable As String
    Public Property UsedRomSize As String
    Public Property NdsHeaderSize As String
    Public Property UnknownOffset As String
    Public Property Reserved3 As String
    Public Property NandEndRomArea As String
    Public Property NandStartRwArea As String
    Public Property Reserved4 As String
    Public Property Reserved5 As String
    Public Property NintendoLogo As String
    Public Property NintendoLogoChecksum As String
    Public Property HeaderChecksum As String
    Public Property DebugRomOffset As String
    Public Property DebugSize As String
    Public Property DebugRamAddress As String
    Public Property Reserved6 As String

    Private Shared ReadOnly CountryCodes As New Dictionary(Of String, String) From {
        {"00", "Normal"},
        {"80", "China"},
        {"40", "Korea"}
    }

    Private Shared ReadOnly MapperCodes As New Dictionary(Of String, String) From {
        {"00", "ROM Only"},
        {"01", "ROM + EEPROM"},
        {"02", "Embedded FLASH"},
        {"03", "NTR built-in SRAM"},
        {"04", "ROM + SRAM"}
    }

    Private Shared ReadOnly PlatformCodes As New Dictionary(Of String, String) From {
        {"00", "NDS"},
        {"02", "NDS+DSi"},
        {"03", "DSi"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 512 Then
            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 12).TrimEnd(ControlChars.NullChar)
            GameCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 12, 4)
            MakerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 16, 2)
            UnitCode = GetMappedValue(PlatformCodes, HeaderBytes(18).ToString())
            EncryptionSeedSelect = HeaderBytes(19).ToString()
            DeviceCapacity = HeaderBytes(20).ToString()
            Reserved1 = BitConverter.ToString(HeaderBytes, 21, 7).Replace("-", " ")
            Reserved2 = HeaderBytes(28).ToString()
            NdsRegion = GetMappedValue(CountryCodes, HeaderBytes(29).ToString())
            RomVersion = HeaderBytes(30).ToString()
            Autostart = HeaderBytes(31).ToString()
            Arm9RomOffset = BitConverter.ToUInt32(HeaderBytes, 32).ToString()
            Arm9EntryAddress = BitConverter.ToUInt32(HeaderBytes, 36).ToString()
            Arm9RamAddress = BitConverter.ToUInt32(HeaderBytes, 40).ToString()
            Arm9Size = BitConverter.ToUInt32(HeaderBytes, 44).ToString()
            Arm7RomOffset = BitConverter.ToUInt32(HeaderBytes, 48).ToString()
            Arm7EntryAddress = BitConverter.ToUInt32(HeaderBytes, 52).ToString()
            Arm7RamAddress = BitConverter.ToUInt32(HeaderBytes, 56).ToString()
            Arm7Size = BitConverter.ToUInt32(HeaderBytes, 60).ToString()
            FntOffset = BitConverter.ToUInt32(HeaderBytes, 64).ToString()
            FntSize = BitConverter.ToUInt32(HeaderBytes, 68).ToString()
            FatOffset = BitConverter.ToUInt32(HeaderBytes, 72).ToString()
            FatSize = BitConverter.ToUInt32(HeaderBytes, 76).ToString()
            Arm9OverlayOffset = BitConverter.ToUInt32(HeaderBytes, 80).ToString()
            Arm9OverlaySize = BitConverter.ToUInt32(HeaderBytes, 84).ToString()
            Arm7OverlayOffset = BitConverter.ToUInt32(HeaderBytes, 88).ToString()
            Arm7OverlaySize = BitConverter.ToUInt32(HeaderBytes, 92).ToString()
            NormalCommandSetting = BitConverter.ToUInt32(HeaderBytes, 96).ToString()
            Key1CommandSetting = BitConverter.ToUInt32(HeaderBytes, 100).ToString()
            IconTitleOffset = BitConverter.ToUInt32(HeaderBytes, 104).ToString()
            SecureAreaChecksum = BitConverter.ToUInt16(HeaderBytes, 108).ToString()
            SecureAreaDelay = BitConverter.ToUInt16(HeaderBytes, 110).ToString()
            Arm9AutoLoadAddress = BitConverter.ToUInt32(HeaderBytes, 112).ToString()
            Arm7AutoLoadAddress = BitConverter.ToUInt32(HeaderBytes, 116).ToString()
            SecureAreaDisable = BitConverter.ToString(HeaderBytes, 120, 8).Replace("-", " ")
            UsedRomSize = BitConverter.ToUInt32(HeaderBytes, 128).ToString()
            NdsHeaderSize = BitConverter.ToUInt32(HeaderBytes, 132).ToString()
            UnknownOffset = BitConverter.ToUInt32(HeaderBytes, 136).ToString()
            Reserved3 = BitConverter.ToString(HeaderBytes, 140, 56).Replace("-", " ")
            NandEndRomArea = BitConverter.ToUInt16(HeaderBytes, 196).ToString()
            NandStartRwArea = BitConverter.ToUInt16(HeaderBytes, 198).ToString()
            Reserved4 = BitConverter.ToString(HeaderBytes, 200, 24).Replace("-", " ")
            Reserved5 = BitConverter.ToString(HeaderBytes, 224, 16).Replace("-", " ")
            NintendoLogo = BitConverter.ToString(HeaderBytes, 240, 156).Replace("-", " ")
            NintendoLogoChecksum = BitConverter.ToUInt16(HeaderBytes, 396).ToString()
            HeaderChecksum = BitConverter.ToUInt16(HeaderBytes, 398).ToString()
            DebugRomOffset = BitConverter.ToUInt32(HeaderBytes, 400).ToString()
            DebugSize = BitConverter.ToUInt32(HeaderBytes, 404).ToString()
            DebugRamAddress = BitConverter.ToUInt32(HeaderBytes, 408).ToString()
            Reserved6 = BitConverter.ToUInt32(HeaderBytes, 412).ToString()
        Else
            GameTitle = ""
            GameCode = ""
            MakerCode = ""
            UnitCode = ""
            EncryptionSeedSelect = ""
            DeviceCapacity = ""
            Reserved1 = ""
            Reserved2 = ""
            NdsRegion = ""
            RomVersion = ""
            Autostart = ""
            Arm9RomOffset = ""
            Arm9EntryAddress = ""
            Arm9RamAddress = ""
            Arm9Size = ""
            Arm7RomOffset = ""
            Arm7EntryAddress = ""
            Arm7RamAddress = ""
            Arm7Size = ""
            FntOffset = ""
            FntSize = ""
            FatOffset = ""
            FatSize = ""
            Arm9OverlayOffset = ""
            Arm9OverlaySize = ""
            Arm7OverlayOffset = ""
            Arm7OverlaySize = ""
            NormalCommandSetting = ""
            Key1CommandSetting = ""
            IconTitleOffset = ""
            SecureAreaChecksum = ""
            SecureAreaDelay = ""
            Arm9AutoLoadAddress = ""
            Arm7AutoLoadAddress = ""
            SecureAreaDisable = ""
            UsedRomSize = ""
            NdsHeaderSize = ""
            UnknownOffset = ""
            Reserved3 = ""
            NandEndRomArea = ""
            NandStartRwArea = ""
            Reserved4 = ""
            Reserved5 = ""
            NintendoLogo = ""
            NintendoLogoChecksum = ""
            HeaderChecksum = ""
            DebugRomOffset = ""
            DebugSize = ""
            DebugRamAddress = ""
            Reserved6 = ""
        End If
    End Sub
End Class

Public Class DsiRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "DSi_"
        Extensions = New String() {".dsi"}
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property GameTitle As String
    Public Property GameCode As String
    Public Property MakerCode As String
    Public Property UnitCode As String
    Public Property EncryptionSeedSelect As String
    Public Property DeviceCapacity As String
    Public Property Reserved1 As String
    Public Property Reserved2 As String
    Public Property NdsRegion As String
    Public Property RomVersion As String
    Public Property Autostart As String
    Public Property Arm9RomOffset As String
    Public Property Arm9EntryAddress As String
    Public Property Arm9RamAddress As String
    Public Property Arm9Size As String
    Public Property Arm7RomOffset As String
    Public Property Arm7EntryAddress As String
    Public Property Arm7RamAddress As String
    Public Property Arm7Size As String
    Public Property FntOffset As String
    Public Property FntSize As String
    Public Property FatOffset As String
    Public Property FatSize As String
    Public Property Arm9OverlayOffset As String
    Public Property Arm9OverlaySize As String
    Public Property Arm7OverlayOffset As String
    Public Property Arm7OverlaySize As String
    Public Property NormalCommandSetting As String
    Public Property Key1CommandSetting As String
    Public Property IconTitleOffset As String
    Public Property SecureAreaChecksum As String
    Public Property SecureAreaDelay As String
    Public Property Arm9AutoLoadAddress As String
    Public Property Arm7AutoLoadAddress As String
    Public Property SecureAreaDisable As String
    Public Property NdsRomSize As String
    Public Property NdsHeaderSize As String
    Public Property Reserved3 As String
    Public Property NintendoLogo As String
    Public Property NintendoLogoChecksum As String
    Public Property HeaderChecksum As String
    Public Property DebugRomOffset As String
    Public Property DebugSize As String
    Public Property DebugRamAddress As String
    Public Property Reserved4 As String

    Public Property GlobalMBK1_5 As String
    Public Property ARM9LocalMBK6_8 As String
    Public Property ARM7LocalMBK6_8 As String
    Public Property GlobalMBK9 As String
    Public Property RegionFlags As String
    Public Property AccessControl As String
    Public Property Scfg_Ext7 As String
    Public Property Flags As String
    Public Property IsModcrypted As String
    Public Property ModcryptKeySelect As String
    Public Property DebugDisabled As String
    Public Property PermitJump As String
    Public Property Reserved5 As String
    Public Property DsiFlags As String
    Public Property Arm9iLoadAddress As String
    Public Property Reserved6 As String
    Public Property Arm9iSize As String
    Public Property Arm7iRomOffset As String
    Public Property Arm7iSdmmcBufferAddress As String
    Public Property Arm7iLoadAddress As String
    Public Property Arm7iSize As String
    Public Property DigestNtrRegionOffset As String
    Public Property DigestNtrRegionSize As String
    Public Property DigestTwlRegionOffset As String
    Public Property DigestTwlRegionSize As String
    Public Property DigestSectorHashtableOffset As String
    Public Property DigestSectorHashtableSize As String
    Public Property DigestBlockHashtableOffset As String
    Public Property DigestBlockHashtableSize As String
    Public Property DigestSectorSize As String
    Public Property DigestBlockSectorCount As String
    Public Property IconSize As String
    Public Property DsiRomSize As String
    Public Property Sdmmc1Size As String
    Public Property EulaVersion As String
    Public Property Sdmmc2Size As String
    Public Property Unknown1 As String
    Public Property Sdmmc3Size As String
    Public Property Sdmmc4Size As String
    Public Property Sdmmc5Size As String
    Public Property Sdmmc6Size As String
    Public Property Arm9iParamTableOffset As String
    Public Property Arm7iParamTableOffset As String
    Public Property Modcrypt1Offset As String
    Public Property Modcrypt1Size As String
    Public Property Modcrypt2Offset As String
    Public Property Modcrypt2Size As String
    Public Property TitleId As String
    Public Property TitleIdFiletype As String
    Public Property TitleIdZero1 As String
    Public Property TitleIdThree As String
    Public Property TitleIdZero2 As String
    Public Property PublicSaveSize As String
    Public Property PrivateSaveSize As String
    Public Property Reserved7 As String
    Public Property AgeRatings As String
    Public Property AgeRatingJapan As String
    Public Property AgeRatingUSA As String
    Public Property AgeRatingReserved1 As String
    Public Property AgeRatingGermany As String
    Public Property AgeRatingEurope As String
    Public Property AgeRatingReserved2 As String
    Public Property AgeRatingPortugal As String
    Public Property AgeRatingUK As String
    Public Property AgeRatingAustralia As String
    Public Property AgeRatingKorea As String

    Private Shared ReadOnly PlatformCodes As New Dictionary(Of String, String) From {
        {"00", "NDS"},
        {"02", "NDS+DSi"},
        {"03", "DSi"}
    }

    Private Shared ReadOnly RegionCodes As New Dictionary(Of String, String) From {
        {"00", "Normal"},
        {"80", "China"},
        {"40", "Korea"},
        {"01", "System Settings Jump"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 512 Then

            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 12).TrimEnd(ControlChars.NullChar)
            GameCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 12, 4)
            MakerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 16, 2)
            UnitCode = GetMappedValue(PlatformCodes, HeaderBytes(18).ToString())
            EncryptionSeedSelect = HeaderBytes(19).ToString()
            DeviceCapacity = HeaderBytes(20).ToString()
            Reserved1 = BitConverter.ToString(HeaderBytes, 21, 7).Replace("-", " ")
            Reserved2 = HeaderBytes(28).ToString()
            NdsRegion = GetMappedValue(RegionCodes, HeaderBytes(29).ToString())
            RomVersion = HeaderBytes(30).ToString()
            Autostart = HeaderBytes(31).ToString()
            Arm9RomOffset = BitConverter.ToUInt32(HeaderBytes, 32).ToString()
            Arm9EntryAddress = BitConverter.ToUInt32(HeaderBytes, 36).ToString()
            Arm9RamAddress = BitConverter.ToUInt32(HeaderBytes, 40).ToString()
            Arm9Size = BitConverter.ToUInt32(HeaderBytes, 44).ToString()
            Arm7RomOffset = BitConverter.ToUInt32(HeaderBytes, 48).ToString()
            Arm7EntryAddress = BitConverter.ToUInt32(HeaderBytes, 52).ToString()
            Arm7RamAddress = BitConverter.ToUInt32(HeaderBytes, 56).ToString()
            Arm7Size = BitConverter.ToUInt32(HeaderBytes, 60).ToString()
            FntOffset = BitConverter.ToUInt32(HeaderBytes, 64).ToString()
            FntSize = BitConverter.ToUInt32(HeaderBytes, 68).ToString()
            FatOffset = BitConverter.ToUInt32(HeaderBytes, 72).ToString()
            FatSize = BitConverter.ToUInt32(HeaderBytes, 76).ToString()
            Arm9OverlayOffset = BitConverter.ToUInt32(HeaderBytes, 80).ToString()
            Arm9OverlaySize = BitConverter.ToUInt32(HeaderBytes, 84).ToString()
            Arm7OverlayOffset = BitConverter.ToUInt32(HeaderBytes, 88).ToString()
            Arm7OverlaySize = BitConverter.ToUInt32(HeaderBytes, 92).ToString()
            NormalCommandSetting = BitConverter.ToUInt32(HeaderBytes, 96).ToString()
            Key1CommandSetting = BitConverter.ToUInt32(HeaderBytes, 100).ToString()
            IconTitleOffset = BitConverter.ToUInt32(HeaderBytes, 104).ToString()
            SecureAreaChecksum = BitConverter.ToUInt16(HeaderBytes, 108).ToString()
            SecureAreaDelay = BitConverter.ToUInt16(HeaderBytes, 110).ToString()
            Arm9AutoLoadAddress = BitConverter.ToUInt32(HeaderBytes, 112).ToString()
            Arm7AutoLoadAddress = BitConverter.ToUInt32(HeaderBytes, 116).ToString()
            SecureAreaDisable = BitConverter.ToString(HeaderBytes, 120, 8).Replace("-", " ")
            NdsRomSize = BitConverter.ToUInt32(HeaderBytes, 128).ToString()
            NdsHeaderSize = BitConverter.ToUInt32(HeaderBytes, 132).ToString()
            Reserved3 = BitConverter.ToString(HeaderBytes, 136, 56).Replace("-", " ")
            NintendoLogo = BitConverter.ToString(HeaderBytes, 192, 156).Replace("-", " ")
            NintendoLogoChecksum = BitConverter.ToUInt16(HeaderBytes, 348).ToString()
            HeaderChecksum = BitConverter.ToUInt16(HeaderBytes, 350).ToString()
            DebugRomOffset = BitConverter.ToUInt32(HeaderBytes, 352).ToString()
            DebugSize = BitConverter.ToUInt32(HeaderBytes, 356).ToString()
            DebugRamAddress = BitConverter.ToUInt32(HeaderBytes, 360).ToString()
            Reserved4 = BitConverter.ToUInt32(HeaderBytes, 364).ToString()

            GlobalMBK1_5 = BitConverter.ToString(HeaderBytes, 384, 20).Replace("-", " ")
            ARM9LocalMBK6_8 = BitConverter.ToString(HeaderBytes, 404, 12).Replace("-", " ")
            ARM7LocalMBK6_8 = BitConverter.ToString(HeaderBytes, 416, 12).Replace("-", " ")
            GlobalMBK9 = BitConverter.ToString(HeaderBytes, 428, 3).Replace("-", " ")
            RegionFlags = BitConverter.ToString(HeaderBytes, 432, 4).Replace("-", "")
            AccessControl = BitConverter.ToString(HeaderBytes, 436, 4).Replace("-", "")
            Scfg_Ext7 = BitConverter.ToString(HeaderBytes, 440, 4).Replace("-", "")
            Flags = HeaderBytes(444).ToString()
            IsModcrypted = ((Convert.ToInt32(Flags) >> 1) And 1).ToString()
            ModcryptKeySelect = ((Convert.ToInt32(Flags) >> 2) And 1).ToString()
            DebugDisabled = ((Convert.ToInt32(Flags) >> 3) And 1).ToString()
            PermitJump = GetMappedValue(RegionCodes, HeaderBytes(445).ToString())
            Reserved5 = BitConverter.ToString(HeaderBytes, 444, 3).Replace("-", " ")
            DsiFlags = HeaderBytes(447).ToString()
            Arm9iLoadAddress = BitConverter.ToUInt32(HeaderBytes, 456).ToString()
            Reserved6 = BitConverter.ToUInt32(HeaderBytes, 452).ToString()
            Arm9iSize = BitConverter.ToUInt32(HeaderBytes, 460).ToString()
            Arm7iRomOffset = BitConverter.ToUInt32(HeaderBytes, 464).ToString()
            Arm7iSdmmcBufferAddress = BitConverter.ToUInt32(HeaderBytes, 468).ToString()
            Arm7iLoadAddress = BitConverter.ToUInt32(HeaderBytes, 472).ToString()
            Arm7iSize = BitConverter.ToUInt32(HeaderBytes, 476).ToString()
            DigestNtrRegionOffset = BitConverter.ToUInt32(HeaderBytes, 480).ToString()
            DigestNtrRegionSize = BitConverter.ToUInt32(HeaderBytes, 484).ToString()
            DigestTwlRegionOffset = BitConverter.ToUInt32(HeaderBytes, 488).ToString()
            DigestTwlRegionSize = BitConverter.ToUInt32(HeaderBytes, 492).ToString()
            DigestSectorHashtableOffset = BitConverter.ToUInt32(HeaderBytes, 496).ToString()
            DigestSectorHashtableSize = BitConverter.ToUInt32(HeaderBytes, 500).ToString()
            DigestBlockHashtableOffset = BitConverter.ToUInt32(HeaderBytes, 504).ToString()
            DigestBlockHashtableSize = BitConverter.ToUInt32(HeaderBytes, 508).ToString()
            DigestSectorSize = BitConverter.ToUInt32(HeaderBytes, 512).ToString()
            DigestBlockSectorCount = BitConverter.ToUInt32(HeaderBytes, 516).ToString()
            IconSize = BitConverter.ToUInt32(HeaderBytes, 520).ToString()
            DsiRomSize = BitConverter.ToUInt32(HeaderBytes, 528).ToString()
            Sdmmc1Size = HeaderBytes(524).ToString()
            EulaVersion = HeaderBytes(526).ToString()
            Sdmmc2Size = HeaderBytes(525).ToString()
            Unknown1 = BitConverter.ToString(HeaderBytes, 527, 5).Replace("-", " ")
            Sdmmc3Size = HeaderBytes(532).ToString()
            Sdmmc4Size = HeaderBytes(533).ToString()
            Sdmmc5Size = HeaderBytes(534).ToString()
            Sdmmc6Size = HeaderBytes(535).ToString()
            Arm9iParamTableOffset = BitConverter.ToUInt32(HeaderBytes, 536).ToString()
            Arm7iParamTableOffset = BitConverter.ToUInt32(HeaderBytes, 540).ToString()
            Modcrypt1Offset = BitConverter.ToUInt32(HeaderBytes, 544).ToString()
            Modcrypt1Size = BitConverter.ToUInt32(HeaderBytes, 548).ToString()
            Modcrypt2Offset = BitConverter.ToUInt32(HeaderBytes, 552).ToString()
            Modcrypt2Size = BitConverter.ToUInt32(HeaderBytes, 556).ToString()
            TitleId = System.Text.Encoding.ASCII.GetString(HeaderBytes, 560, 4)
            TitleIdFiletype = HeaderBytes(564).ToString()
            TitleIdZero1 = HeaderBytes(565).ToString()
            TitleIdThree = HeaderBytes(566).ToString()
            TitleIdZero2 = HeaderBytes(567).ToString()
            PublicSaveSize = BitConverter.ToUInt32(HeaderBytes, 568).ToString()
            PrivateSaveSize = BitConverter.ToUInt32(HeaderBytes, 572).ToString()
            Reserved7 = BitConverter.ToString(HeaderBytes, 576, 176).Replace("-", " ")

            AgeRatings = BitConverter.ToString(HeaderBytes, 752, 16).Replace("-", " ")
            AgeRatingJapan = HeaderBytes(752).ToString()
            AgeRatingUSA = HeaderBytes(753).ToString()
            AgeRatingReserved1 = HeaderBytes(754).ToString()
            AgeRatingGermany = HeaderBytes(755).ToString()
            AgeRatingEurope = HeaderBytes(756).ToString()
            AgeRatingReserved2 = HeaderBytes(757).ToString()
            AgeRatingPortugal = HeaderBytes(758).ToString()
            AgeRatingUK = HeaderBytes(759).ToString()
            AgeRatingAustralia = HeaderBytes(760).ToString()
            AgeRatingKorea = HeaderBytes(761).ToString()
        Else
            GameTitle = ""
            GameCode = ""
            MakerCode = ""
            UnitCode = ""
            EncryptionSeedSelect = ""
            DeviceCapacity = ""
            Reserved1 = ""
            Reserved2 = ""
            NdsRegion = ""
            RomVersion = ""
            Autostart = ""
            Arm9RomOffset = ""
            Arm9EntryAddress = ""
            Arm9RamAddress = ""
            Arm9Size = ""
            Arm7RomOffset = ""
            Arm7EntryAddress = ""
            Arm7RamAddress = ""
            Arm7Size = ""
            FntOffset = ""
            FntSize = ""
            FatOffset = ""
            FatSize = ""
            Arm9OverlayOffset = ""
            Arm9OverlaySize = ""
            Arm7OverlayOffset = ""
            Arm7OverlaySize = ""
            NormalCommandSetting = ""
            Key1CommandSetting = ""
            IconTitleOffset = ""
            SecureAreaChecksum = ""
            SecureAreaDelay = ""
            Arm9AutoLoadAddress = ""
            Arm7AutoLoadAddress = ""
            SecureAreaDisable = ""
            NdsRomSize = ""
            NdsHeaderSize = ""
            Reserved3 = ""
            NintendoLogo = ""
            NintendoLogoChecksum = ""
            HeaderChecksum = ""
            DebugRomOffset = ""
            DebugSize = ""
            DebugRamAddress = ""
            Reserved4 = ""
            GlobalMBK1_5 = ""
            ARM9LocalMBK6_8 = ""
            ARM7LocalMBK6_8 = ""
            GlobalMBK9 = ""
            RegionFlags = ""
            AccessControl = ""
            Scfg_Ext7 = ""
            Flags = ""
            IsModcrypted = ""
            ModcryptKeySelect = ""
            DebugDisabled = ""
            PermitJump = ""
            Reserved5 = ""
            DsiFlags = ""
            Arm9iLoadAddress = ""
            Reserved6 = ""
            Arm9iSize = ""
            Arm7iRomOffset = ""
            Arm7iSdmmcBufferAddress = ""
            Arm7iLoadAddress = ""
            Arm7iSize = ""
            DigestNtrRegionOffset = ""
            DigestNtrRegionSize = ""
            DigestTwlRegionOffset = ""
            DigestTwlRegionSize = ""
            DigestSectorHashtableOffset = ""
            DigestSectorHashtableSize = ""
            DigestBlockHashtableOffset = ""
            DigestBlockHashtableSize = ""
            DigestSectorSize = ""
            DigestBlockSectorCount = ""
            IconSize = ""
            DsiRomSize = ""
            Sdmmc1Size = ""
            EulaVersion = ""
            Sdmmc2Size = ""
            Unknown1 = ""
            Sdmmc3Size = ""
            Sdmmc4Size = ""
            Sdmmc5Size = ""
            Sdmmc6Size = ""
            Arm9iParamTableOffset = ""
            Arm7iParamTableOffset = ""
            Modcrypt1Offset = ""
            Modcrypt1Size = ""
            Modcrypt2Offset = ""
            Modcrypt2Size = ""
            TitleId = ""
            TitleIdFiletype = ""
            TitleIdZero1 = ""
            TitleIdThree = ""
            TitleIdZero2 = ""
            PublicSaveSize = ""
            PrivateSaveSize = ""
            Reserved7 = ""
            AgeRatings = ""
            AgeRatingJapan = ""
            AgeRatingUSA = ""
            AgeRatingReserved1 = ""
            AgeRatingGermany = ""
            AgeRatingEurope = ""
            AgeRatingReserved2 = ""
            AgeRatingPortugal = ""
            AgeRatingUK = ""
            AgeRatingAustralia = ""
            AgeRatingKorea = ""
        End If
    End Sub
End Class

Public Class PceHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "PCE_"
        Extensions = New String() {".pce"}
        HeaderSize = 72
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property Magic As String
    Public Property Version As String
    Public Property RomSize As String
    Public Property MapperType As String
    Public Property HardwareType As String

    Public Property GameTitle As String
    Public Property ProductName As String
    Public Property ManufacturerID As String
    Public Property GameID As String
    Public Property GameVersion As String
    Public Property TerminalType As String
    Public Property GameDataSize As String
    Public Property Reserved As String

    Private Shared ReadOnly TerminalTypes As New Dictionary(Of String, String) From {
        {"00", "PC Engine"},
        {"01", "TurboGrafx"},
        {"02", "SuperGrafx"},
        {"03", "PC Engine Duo"},
        {"04", "TurboExpress"},
        {"05", "PC Engine LT"}
    }

    Private Shared ReadOnly HardwareTypes As New Dictionary(Of String, String) From {
        {"00", "Standard PC Engine Card"},
        {"01", "Arcade Card Compatible"}
    }

    Private Shared ReadOnly MapperTypes As New Dictionary(Of String, String) From {
        {"00", "Standard PC Engine Card"},
        {"01", "Super CD-ROM2"},
        {"02", "Arcade Card"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= HeaderSize Then

            Magic = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 4)
            Version = BitConverter.ToUInt16(HeaderBytes, 4).ToString()
            RomSize = (CInt("&H" & BitConverter.ToUInt16(HeaderBytes, 6).ToString()) << 9).ToString()
            MapperType = GetMappedValue(MapperTypes, HeaderBytes(8).ToString())
            HardwareType = GetMappedValue(HardwareTypes, HeaderBytes(9).ToString())

            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 8, 16).Trim()
            ProductName = System.Text.Encoding.ASCII.GetString(HeaderBytes, 24, 16).Trim()
            ManufacturerID = System.Text.Encoding.ASCII.GetString(HeaderBytes, 40, 4)
            GameID = BitConverter.ToUInt16(HeaderBytes, 44).ToString()
            GameVersion = HeaderBytes(46).ToString()
            TerminalType = GetMappedValue(TerminalTypes, HeaderBytes(47).ToString())
            GameDataSize = (BitConverter.ToUInt16(HeaderBytes, 48) * 8).ToString() & " KB"
            Reserved = BitConverter.ToString(HeaderBytes, 50, 22).Replace("-", " ")
        Else
            Magic = ""
            Version = ""
            RomSize = ""
            MapperType = ""
            HardwareType = ""
            GameTitle = ""
            ProductName = ""
            ManufacturerID = ""
            GameID = ""
            GameVersion = ""
            TerminalType = ""
            GameDataSize = ""
            Reserved = ""
        End If
    End Sub
End Class

Public Class MegaDriveHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "MD_"
        Extensions = New String() {".md", ".bin"}
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    Public Property ConsoleRegion As String
    Public Property CopyrightNotice As String
    Public Property DomesticName As String
    Public Property OverseasName As String
    Public Property Reserved1 As String
    Public Property ProductType As String
    Public Property ProductCode As String
    Public Property Checksum As String
    Public Property DeviceType As String
    Public Property Reserved2 As String
    Public Property SoftwareVersion As String
    Public Property Reserved3 As String
    Public Property SramCode As String
    Public Property ModemCode As String
    Public Property Reserved4 As String
    Public Property RegionCode As String
    Public Property Reserved5 As String
    Public Property RomStartAddress As String
    Public Property RomEndAddress As String
    Public Property RamStartAddress As String

    Private Shared ReadOnly ConsoleRegions As New Dictionary(Of String, String) From {
        {"4A", "Japan"},
        {"45", "Europe"},
        {"01", "USA"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= HeaderSize Then
            ConsoleRegion = GetMappedValue(ConsoleRegions, System.Text.Encoding.ASCII.GetString(HeaderBytes, 256, 2))
            CopyrightNotice = System.Text.Encoding.ASCII.GetString(HeaderBytes, 258, 16).Trim()
            DomesticName = System.Text.Encoding.ASCII.GetString(HeaderBytes, 274, 48).Trim()
            OverseasName = System.Text.Encoding.ASCII.GetString(HeaderBytes, 322, 48).Trim()
            Reserved1 = BitConverter.ToString(HeaderBytes, 370, 14).Replace("-", " ")
            ProductType = System.Text.Encoding.ASCII.GetString(HeaderBytes, 384, 12).Trim()
            ProductCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 396, 2).Trim()
            Checksum = BitConverter.ToUInt16(HeaderBytes, 398).ToString()
            DeviceType = System.Text.Encoding.ASCII.GetString(HeaderBytes, 400, 2).Trim()
            Reserved2 = BitConverter.ToString(HeaderBytes, 402, 14).Replace("-", " ")
            SoftwareVersion = System.Text.Encoding.ASCII.GetString(HeaderBytes, 416, 16).Trim()
            Reserved3 = BitConverter.ToString(HeaderBytes, 432, 4).Replace("-", " ")
            SramCode = HeaderBytes(436).ToString()
            ModemCode = HeaderBytes(437).ToString()
            Reserved4 = BitConverter.ToString(HeaderBytes, 438, 58).Replace("-", " ")
            RegionCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 496, 2).Trim()
            Reserved5 = BitConverter.ToString(HeaderBytes, 498, 2).Replace("-", " ")
            RomStartAddress = BitConverter.ToUInt32(HeaderBytes, 500).ToString()
            RomEndAddress = BitConverter.ToUInt32(HeaderBytes, 504).ToString()
            RamStartAddress = BitConverter.ToUInt32(HeaderBytes, 508).ToString()
        Else
            ConsoleRegion = ""
            CopyrightNotice = ""
            DomesticName = ""
            OverseasName = ""
            Reserved1 = ""
            ProductType = ""
            ProductCode = ""
            Checksum = ""
            DeviceType = ""
            Reserved2 = ""
            SoftwareVersion = ""
            Reserved3 = ""
            SramCode = ""
            ModemCode = ""
            Reserved4 = ""
            RegionCode = ""
            Reserved5 = ""
            RomStartAddress = ""
            RomEndAddress = ""
            RamStartAddress = ""
        End If
    End Sub
End Class

