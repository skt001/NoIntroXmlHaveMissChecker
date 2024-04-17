' 親クラス
Imports System.Text

Public MustInherit Class RomHeader
    ' 共通のプロパティ
    Public Property Prefix As String
    Public Property HeaderSize As Integer
    Public Property HeaderBytes As Byte()

    Public Sub New()
        Prefix = ""
        HeaderSize = 0
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' 共通の関数
    Public Function GetHeaderInfo() As String
        ' ヘッダー情報を文字列で返す
        Return BitConverter.ToString(HeaderBytes, 0, HeaderSize).Replace("-", "")
    End Function

    Public Function GetMappedValue(translation As Dictionary(Of String, String), key As String) As String
        ' キーに対応する文字列値を取得する
        If translation.ContainsKey(key) Then
            Return translation(key)
        Else
            Return $"Unknown ({key})"
        End If
    End Function

    Public Function GetMappedCharValue(translation As Dictionary(Of Char, String), key As Char) As String
        ' キーに対応する文字列値を取得する
        If translation.ContainsKey(key) Then
            Return translation(key)
        Else
            Return $"Unknown ({key})"
        End If
    End Function

    Public MustOverride Sub SetHeaderInfo()
End Class

' 子クラス（NES用）
Public Class NesRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "NES_"
        HeaderSize = 16
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' NES固有のプロパティ
    Public Property MagicNumber As String
    Public Property PrgRomSize As String
    Public Property ChrRomSize As String
    Public Property MapperLowerNibble As String
    Public Property MapperUpperNibble As String
    Public Property MapperType As String
    Public Property MapperTypeHighNibble As String
    Public Property MirroringType As String
    Public Property Battery As String
    Public Property Trainer As String
    Public Property VsSystem As String
    Public Property Nes20 As String
    Public Property PrgRamSize As String
    Public Property TvSystem1 As String
    Public Property TvSystem2 As String
    Public Property PrgRamPresent As String
    Public Property UnusedPadding As String

    ' NES固有の変換テーブル
    Private Shared ReadOnly NesMapperTypes As New Dictionary(Of String, String) From {
        {"0", "NROM"}, {"1", "MMC1"}, {"2", "UNROM"}, {"3", "CNROM"}, {"4", "MMC3"}, {"5", "MMC5"}, {"6", "FFE F4xxx"}, {"7", "AOROM"}, {"8", "FFE F3xxx"}, {"9", "MMC2"}, {"10", "MMC4"}, {"11", "Color Dreams"}, {"12", "FFE F6xxx"}, {"13", "CPROM"}, {"15", "100-in-1"}, {"16", "Bandai"}, {"17", "FFE F8xxx"}, {"18", "Jaleco SS8806"}, {"19", "Namcot 106"}, {"20", "Famicom Disk System"}, {"21", "Konami VRC4"}, {"22", "Konami VRC2"}, {"23", "Konami VRC2"}, {"24", "Konami VRC6"}, {"25", "Konami VRC4"}, {"32", "Irem G-101"}, {"33", "Taito TC0190"}, {"34", "Nina-1"}, {"64", "Tengen RAMBO-1"}, {"65", "Irem H-3001"}, {"66", "GxROM"}, {"67", "Sunsoft 3"}, {"68", "Sunsoft 4"}, {"69", "Sunsoft FME-7"}, {"71", "Camerica"}, {"78", "Irem 74HC161/32"}, {"79", "AVE Nina-3"}, {"81", "AVE Nina-6"}, {"91", "Pirate HK-SF3"}, {"92", "Pirate Qi-Wang"}, {"97", "Irem TAM-S1"}
    }

    Private Shared ReadOnly NesMapperTypesHighNibble As New Dictionary(Of String, String) From {
        {"0", "No NES 2.0 Extension"}, {"1", "Nintendo MMC1"}, {"2", "Nintendo MMC2"}, {"3", "Nintendo MMC3"}, {"4", "Nintendo MMC4"}, {"5", "Nintendo MMC5"}, {"6", "FFE F4xxx"}, {"7", "Nintendo AxROM"}, {"8", "FFE F3xxx"}, {"9", "Nintendo MMC2"}, {"10", "Nintendo MMC4"}, {"11", "Color Dreams Chip"}, {"12", "FFE F6xxx"}, {"13", "Nintendo CPROM"}, {"14", "Nintendo AxROM"}, {"15", "100-in-1 Switch"}, {"16", "Bandai Chip"}, {"17", "FFE F8xxx"}, {"18", "Jaleco SS8806 Chip"}, {"19", "Namcot 106 Chip"}, {"20", "Nintendo DiskSystem"}, {"21", "Konami VRC4a"}, {"22", "Konami VRC2a"}, {"23", "Konami VRC2b"}, {"24", "Konami VRC6"}, {"25", "Konami VRC4b"}, {"26", "Konami VRC4c"}, {"27", "Konami VRC4e"}, {"28", "Konami VRC4f"}, {"29", "Konami VRC4d"}, {"30", "Konami VRC6b"}, {"31", "Konami VRC6a"}
    }

    Private Shared ReadOnly NesMirroringTypes As New Dictionary(Of String, String) From {
        {"0", "Horizontal"}, {"1", "Vertical"}, {"2", "Four-screen VRAM"}, {"3", "Four-screen VRAM (Last Bank)"}
    }

    Private Shared ReadOnly NesBatteryTypes As New Dictionary(Of String, String) From {
        {"0", "No Battery"}, {"1", "Battery Backed"}
    }

    Private Shared ReadOnly NesTrainerTypes As New Dictionary(Of String, String) From {
        {"0", "No Trainer"}, {"1", "512-byte Trainer"}
    }

    Private Shared ReadOnly NesVsTypes As New Dictionary(Of String, String) From {
        {"0", "Regular Cartridge"}, {"1", "VS-System Cartridge"}
    }

    Private Shared ReadOnly NesPlaychoiceTypes As New Dictionary(Of String, String) From {
        {"0", "Regular Cartridge"}, {"1", "PlayChoice-10 Cartridge"}
    }

    Private Shared ReadOnly NesTelevisionTypes As New Dictionary(Of String, String) From {
        {"0", "NTSC"}, {"1", "PAL"}, {"2", "Dual Compatible"}, {"3", "Dual Compatible (NTSC)"}, {"4", "Dual Compatible (PAL)"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 16 Then
            MagicNumber = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 4)
            PrgRomSize = HeaderBytes(4).ToString()
            ChrRomSize = HeaderBytes(5).ToString()
            MapperLowerNibble = (HeaderBytes(6) And &HF).ToString()
            MapperUpperNibble = ((HeaderBytes(7) And &HF0) >> 4).ToString()
            MapperType = GetMappedValue(NesMapperTypes, MapperLowerNibble)
            MapperTypeHighNibble = GetMappedValue(NesMapperTypesHighNibble, MapperUpperNibble)
            MirroringType = GetMappedValue(NesMirroringTypes, (HeaderBytes(6) And &H3).ToString())
            Battery = GetMappedValue(NesBatteryTypes, ((HeaderBytes(6) And &H2) >> 1).ToString())
            Trainer = GetMappedValue(NesTrainerTypes, ((HeaderBytes(6) And &H4) >> 2).ToString())
            VsSystem = GetMappedValue(NesVsTypes, (HeaderBytes(7) And &H1).ToString())
            Nes20 = GetMappedValue(NesPlaychoiceTypes, (HeaderBytes(7) And &H2).ToString())
            PrgRamSize = HeaderBytes(8).ToString()
            TvSystem1 = GetMappedValue(NesTelevisionTypes, (HeaderBytes(9) And &H1).ToString())
            TvSystem2 = GetMappedValue(NesTelevisionTypes, ((HeaderBytes(10) And &H2) >> 1).ToString())
            PrgRamPresent = ((HeaderBytes(10) And &H10) <> 0).ToString()
            UnusedPadding = GetHeaderInfo().Substring(22)
        Else
            ' ヘッダーが不完全な場合の処理
            ' ...
        End If
    End Sub
End Class

' 子クラス（SNES用）
Public Class SnesRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "SNES_"
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' SNES固有のプロパティ
    Public Property GameTitle As String
    Public Property MappingType As String
    Public Property CartridgeType As String
    Public Property RomSize As String
    Public Property RamSize As String
    Public Property DestinationCode As String
    Public Property MaskRomVersion As String
    Public Property ComplementCheck As String
    Public Property Checksum As String

    ' SNES固有の変換テーブル
    Private Shared ReadOnly SnesRomSizes As New Dictionary(Of String, String) From {
        {"9", "48 Mbit"}, {"8", "32 Mbit"}, {"7", "24 Mbit"}, {"6", "16 Mbit"}, {"5", "8 Mbit"}, {"4", "4 Mbit"}, {"3", "2 Mbit"}, {"2", "1 Mbit"}, {"1", "512 Kbit"}, {"0", "256 Kbit"}
    }

    Private Shared ReadOnly SnesRamSizes As New Dictionary(Of String, String) From {
        {"5", "64 Kbits"}, {"4", "32 Kbits"}, {"3", "16 Kbits"}, {"2", "8 Kbits"}, {"1", "4 Kbits"}, {"0", "No RAM"}
    }

    Private Shared ReadOnly SnesDestinationCodes As New Dictionary(Of String, String) From {
        {"0", "Japan"}, {"1", "USA"}, {"2", "Europe"}, {"3", "Scandinavia"}, {"4", "Finland"}, {"5", "Denmark"}, {"6", "France"}, {"7", "Holland"}, {"8", "Spain"}, {"9", "Germany"}, {"10", "Italy"}, {"11", "China"}, {"13", "Korea"}, {"15", "Canada"}
    }

    Private Shared ReadOnly SnesMappingTypes As New Dictionary(Of String, String) From {
        {"0", "LoROM"}, {"1", "HiROM"}, {"2", "LoROM (SA-1)"}, {"3", "LoROM (FS-A)"}, {"4", "LoROM (FS-B)"}, {"5", "LoROM (FS-C)"}, {"6", "LoROM (FastLoROM)"}, {"7", "LoROM (FastHiROM)"}, {"8", "HiROM (FastLoROM)"}, {"9", "HiROM (FastHiROM)"}, {"11", "ExHiROM"}, {"12", "ExHiROM (SA-1)"}, {"20", "HiROM (FS-A)"}, {"21", "HiROM (FS-B)"}, {"22", "HiROM (FS-C)"}, {"48", "SPC7110"}, {"49", "SPC7110 (RTC)"}, {"50", "SPC7110 (RTC/SRAM)"}, {"52", "SA1 (x2)"}, {"53", "SA1 (x4)"}, {"67", "CX4"}, {"69", "SPC7110 (DRAM)"}, {"81", "SDD1"}, {"83", "S-DD1 (CX4)"}, {"84", "S-RTC"}, {"85", "S-RTC (RTC)"}, {"88", "GameBoy (PocketCam)"}, {"229", "MultiROM"}, {"230", "MultiROM (CX4)"}, {"231", "MultiROM (OBC1)"}, {"243", "Pocket Voice (GB)"}, {"244", "SPC7110 (DR-X2)"}, {"245", "StreamerSPC7110"}, {"246", "StreamerSPC7110 (RTC)"}, {"247", "StreamerSPC7110 (RTC/SRAM)"}, {"248", "SPC7110 (DR-X2_RTC/SRAM)"}, {"249", "Super MMC"}, {"26", "Super Gameboy 2"}, {"255", "Custom Chipset"}
    }

    Private Shared ReadOnly SnesCartridgeTypes As New Dictionary(Of String, String) From {
        {"0", "ROM Only"},
        {"1", "ROM + RAM"},
        {"2", "ROM + RAM + Battery"},
        {"3", "ROM + Co-Processor"},
        {"4", "ROM + Co-Processor + RAM"},
        {"5", "ROM + Co-Processor + RAM + Battery"},
        {"6", "ROM + Co-Processor + Battery"},
        {"7", "ROM + Co-Processor + RAM + Battery + RTC"},
        {"8", "ROM + RAM + MMC3"},
        {"9", "ROM + RAM + Battery + MMC3"},
        {"14", "ROM + RAM + Battery + MMC3 + RTC"},
        {"52", "ROM + RAM + SA-1"},
        {"53", "ROM + RAM + Battery + SA-1"},
        {"67", "ROM + Co-Processor (CX4)"},
        {"69", "ROM + Co-Processor (CX4) + RAM"},
        {"81", "ROM + Co-Processor (SDD-1)"},
        {"83", "ROM + Co-Processor (SDD-1) + Co-Processor (CX4)"},
        {"84", "ROM + Co-Processor (SDD-1) + RAM"},
        {"85", "ROM + Co-Processor (SDD-1) + RAM + RTC"},
        {"89", "ROM + Pocket Voice"},
        {"243", "ROM + Pocket Voice + RAM"},
        {"245", "ROM + Co-Processor (SPC7110)"},
        {"246", "ROM + Co-Processor (SPC7110) + RTC"},
        {"249", "ROM + MMC3"},
        {"48", "ROM + Co-Processor (SPC7110)"},
        {"49", "ROM + Co-Processor (SPC7110) + RTC"},
        {"50", "ROM + Co-Processor (SPC7110) + RAM + RTC"},
        {"255", "ROM + Custom Chipset"}
    }

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 512 Then
            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 21).Trim()
            MappingType = GetMappedValue(SnesMappingTypes, HeaderBytes(21).ToString())
            CartridgeType = GetMappedValue(SnesCartridgeTypes, HeaderBytes(22).ToString())
            RomSize = GetMappedValue(SnesRomSizes, HeaderBytes(23).ToString())
            RamSize = GetMappedValue(SnesRamSizes, HeaderBytes(24).ToString())
            DestinationCode = GetMappedValue(SnesDestinationCodes, HeaderBytes(25).ToString())
            MaskRomVersion = HeaderBytes(27).ToString()
            ComplementCheck = BitConverter.ToUInt16(HeaderBytes, 28).ToString()
            Checksum = BitConverter.ToUInt16(HeaderBytes, 30).ToString()
        Else
            ' ヘッダーが不完全な場合の処理
            ' ...
        End If
    End Sub
End Class

' 子クラス（N64用）
Public Class N64RomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "N64_"
        HeaderSize = 64
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' N64固有のプロパティ
    Public Property MagicNumber As String
    Public Property ClockRate As String
    Public Property ProgramCounter As String
    Public Property Release As String
    Public Property CRC1 As String
    Public Property CRC2 As String
    Public Property Unknown1 As String
    Public Property ImageName As String
    Public Property Unknown2 As String
    Public Property ManufacturerID As String
    Public Property CartridgeID As String
    Public Property CartridgeIDString As String
    Public Property CountryCode As String
    Public Property CountryCodeString As String
    Public Property ImageRegion As String
    Public Property ImageType As String
    Public Property CICType As String
    Public Property Version As String

    ' N64固有の変換テーブル
    Private Shared ReadOnly N64CountryCodes As New Dictionary(Of String, String) From {
    {"37", "Beta"}, {"41", "Asian (NTSC)"}, {"44", "German"}, {"45", "North America"}, {"46", "French"}, {"49", "Italian"}, {"50", "Japanese"}, {"80", "European (Basic spec.)"}, {"83", "Spanish"}, {"85", "Australian"}, {"89", "European"}, {"97", "Asian (PAL)"}
}

    Private Shared ReadOnly N64CartridgeIDs As New Dictionary(Of String, String) From {
    {"0000", "ROM Only"}, {"0001", "Nintendo DD"}, {"0003", "ROM + Modem Cart"}, {"0004", "ROM + Disk Drive"}, {"0005", "Modem Cart"}, {"0006", "Modem Cart + Disk Drive"}, {"000A", "Aleck64"}, {"000B", "Aleck64 + Capture Cassette"}, {"000C", "ROM + EPAK"}, {"000F", "Custom"}, {"0010", "ROM + CIC-6103 (7102)"}, {"0011", "ROM + CIC-7101"}, {"0012", "ROM + CIC-7103"}, {"0013", "ROM + CIC-6106"}, {"0014", "ROM + CIC-6105"}, {"0015", "64DD + CIC-7101"}, {"0016", "64DD + CIC-6106"}, {"0017", "ROM + MX"}, {"0018", "ROM + CIC-6103 (7102) + Tamper Resistant Case"}, {"0019", "64DD + CIC-7103"}, {"1000", "Demo Kit"}, {"1001", "Demo Kit (NUS)"}, {"1002", "Demo Kit (NU64)"}, {"2000", "Test Kit"}, {"2001", "Test Kit (NUS)"}, {"2010", "Development Kit"}, {"2011", "Development Kit (NUS)"}, {"2012", "Development Kit (NU64)"}, {"2013", "Development Kit (Partner-N)"}, {"3000", "Emulator"}, {"3001", "Emulator (NUS)"}, {"3010", "Emulator (debug)"}, {"3011", "Emulator (debug, NUS)"}, {"4000", "Prototype/Test"}, {"5000", "Emulator (Kiosk)"}
}

    Private Shared ReadOnly N64ImageRegions As New Dictionary(Of String, String) From {
    {"37", "Beta"}, {"41", "USA/Japan"}, {"44", "Germany"}, {"45", "USA"}, {"46", "France"}, {"49", "Italy"}, {"50", "Japan"}, {"80", "Europe"}, {"83", "Spain"}, {"85", "Australia"}, {"89", "Australia"}, {"97", "USA/Japan"}
}

    Private Shared ReadOnly N64ImageTypes As New Dictionary(Of String, String) From {
    {"16672", "Cartridge"}, {"17184", "64DD"}, {"17440", "Cartridge (Aleck64)"}, {"17696", "64DD (Dev)"}, {"18464", "cartridge MultiTaped"}, {"19520", "Cartridge Flash"}, {"20608", "Cartridge Flash (Dev)"}, {"22560", "Unknown"}, {"24752", "Cartridge (Titus)"}
}

    Private Shared ReadOnly N64Cics As New Dictionary(Of String, String) From {
    {"0", "CIC-6102"}, {"1", "CIC-7101"}, {"2", "CIC-7102"}, {"3", "CIC-6103"}, {"4", "CIC-6105"}, {"5", "CIC-6106"}, {"6", "Unknown (0x6)"}, {"7", "Unknown (0x7)"}, {"8", "Unknown (0x8)"}, {"9", "Unknown (0x9)"}, {"10", "Unknown (0xA)"}, {"11", "Unknown (0xB)"}, {"12", "Unknown (0xC)"}, {"13", "Unknown (0xD)"}, {"14", "Unknown (0xE)"}, {"15", "Unknown (0xF)"},
    {"16", "CIC-6101"}, {"17", "CIC-NUS-6101"}, {"18", "CIC-NUS-6102"}, {"19", "CIC-NUS-6103"}, {"20", "CIC-NUS-7101"}, {"21", "CIC-NUS-7102"}, {"22", "Unknown (0x16)"}, {"23", "Unknown (0x17)"}, {"24", "Unknown (0x18)"}, {"25", "Unknown (0x19)"}, {"26", "Unknown (0x1A)"}, {"27", "Unknown (0x1B)"}, {"28", "Unknown (0x1C)"}, {"29", "Unknown (0x1D)"}, {"30", "Unknown (0x1E)"}, {"31", "Unknown (0x1F)"},
    {"32", "CIC-6101-1"}, {"33", "CIC-7101-1"}, {"34", "CIC-7102-1"}, {"35", "CIC-6103-1"}, {"36", "CIC-6105-1"}, {"37", "CIC-6106-1"}, {"38", "Unknown (0x26)"}, {"39", "Unknown (0x27)"}, {"40", "Unknown (0x28)"}, {"41", "Unknown (0x29)"}, {"42", "Unknown (0x2A)"}, {"43", "Unknown (0x2B)"}, {"44", "Unknown (0x2C)"}, {"45", "Unknown (0x2D)"}, {"46", "Unknown (0x2E)"}, {"47", "Unknown (0x2F)"},
    {"48", "CIC-5101"}, {"49", "CIC-3195A"}, {"50", "CIC-3195B"}, {"51", "Unknown (0x33)"}, {"52", "Unknown (0x34)"}, {"53", "Unknown (0x35)"}, {"54", "Unknown (0x36)"}, {"55", "Unknown (0x37)"}, {"56", "Unknown (0x38)"}, {"57", "Unknown (0x39)"}, {"58", "Unknown (0x3A)"}, {"59", "Unknown (0x3B)"}, {"60", "Unknown (0x3C)"}, {"61", "Unknown (0x3D)"}, {"62", "Unknown (0x3E)"}, {"63", "Unknown (0x3F)"},
    {"64", "Unknown (0x40)"}, {"65", "Unknown (0x41)"}, {"66", "Unknown (0x42)"}, {"67", "Unknown (0x43)"}, {"68", "Unknown (0x44)"}, {"69", "Unknown (0x45)"}, {"70", "Unknown (0x46)"}, {"71", "Unknown (0x47)"}, {"72", "Unknown (0x48)"}, {"73", "Unknown (0x49)"}, {"74", "Unknown (0x4A)"}, {"75", "Unknown (0x4B)"}, {"76", "Unknown (0x4C)"}, {"77", "Unknown (0x4D)"}, {"78", "Unknown (0x4E)"}, {"79", "Unknown (0x4F)"}
}

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 64 Then
            MagicNumber = BitConverter.ToUInt32(HeaderBytes, 0).ToString()
            ClockRate = BitConverter.ToUInt32(HeaderBytes, 4).ToString()
            ProgramCounter = BitConverter.ToUInt32(HeaderBytes, 8).ToString()
            Release = BitConverter.ToUInt32(HeaderBytes, 12).ToString()
            CRC1 = BitConverter.ToUInt32(HeaderBytes, 16).ToString()
            CRC2 = BitConverter.ToUInt32(HeaderBytes, 20).ToString()
            Unknown1 = GetHeaderInfo().Substring(48, 16)
            ImageName = System.Text.Encoding.ASCII.GetString(HeaderBytes, 32, 20).Trim()
            Unknown2 = BitConverter.ToUInt32(HeaderBytes, 52).ToString()
            ManufacturerID = BitConverter.ToUInt16(HeaderBytes, 56).ToString()
            CartridgeID = BitConverter.ToString(HeaderBytes, 58, 2).Replace("-", "")
            CartridgeIDString = GetMappedValue(N64CartridgeIDs, CartridgeID)
            CountryCode = HeaderBytes(60).ToString()
            CountryCodeString = GetMappedValue(N64CountryCodes, CountryCode)
            ImageRegion = GetMappedValue(N64ImageRegions, CountryCode)
            ImageType = GetMappedValue(N64ImageTypes, MagicNumber)
            CICType = GetMappedValue(N64Cics, HeaderBytes(62).ToString())
            Version = HeaderBytes(63).ToString()
        Else
            ' ヘッダーが不完全な場合の処理
            ' ...
        End If
    End Sub
End Class

' 子クラス（GBA用）
Public Class GbaRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "GBA_"
        HeaderSize = 192
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' GBA固有のプロパティ
    Public Property EntryPoint As String
    Public Property NintendoLogo As String
    Public Property GameTitle As String
    Public Property GameCode As String
    Public Property GameCodeRegion As String
    Public Property MakerCode As String
    Public Property MainUnitCode As String
    Public Property MainUnitCodeString As String
    Public Property DeviceType As String
    Public Property DeviceTypeString As String
    Public Property Reserved1 As String
    Public Property SoftwareVersion As String
    Public Property ComplementCheck As String
    Public Property Reserved2 As String

    ' GBA固有の変換テーブル
    Private Shared ReadOnly GbaMainUnitCodes As New Dictionary(Of String, String) From {
    {"0", "GBA"}, {"1", "GBA SP"}, {"2", "DS"}, {"3", "DS Lite"}, {"4", "DSi"}, {"5", "DSi XL"}
}

    Private Shared ReadOnly GbaDeviceTypes As New Dictionary(Of String, String) From {
    {"0", "GBA"}, {"1", "GBC"}, {"128", "GBA + GBC"}, {"192", "e-Reader"}
}

    Private Shared ReadOnly GbaGamecodeRegions As New Dictionary(Of Char, String) From {
    {"A"c, "All (Normal)"}, {"B"c, "All (Debug)"}, {"J"c, "Japan (Normal)"}, {"K"c, "Korea (Normal)"}, {"L"c, "Japan (Debug)"}, {"M"c, "Korea (Debug)"}, {"P"c, "Europe (Normal)"}, {"Q"c, "Europe (Debug)"}, {"R"c, "USA (Normal)"}, {"S"c, "USA (Debug)"}, {"U"c, "Australia (Normal)"}, {"V"c, "Australia (Debug)"}, {"W"c, "China (Debug)"}, {"X"c, "China (Normal)"}
}

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 192 Then
            EntryPoint = BitConverter.ToUInt32(HeaderBytes, 0).ToString()
            NintendoLogo = GetHeaderInfo().Substring(8, 312)
            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 160, 12).Trim()
            GameCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 172, 4)
            GameCodeRegion = GetMappedCharValue(GbaGamecodeRegions, ChrW(HeaderBytes(172)))
            MakerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 176, 2)
            MainUnitCode = HeaderBytes(179).ToString()
            MainUnitCodeString = GetMappedValue(GbaMainUnitCodes, MainUnitCode)
            DeviceType = HeaderBytes(180).ToString()
            DeviceTypeString = GetMappedValue(GbaDeviceTypes, DeviceType)
            Reserved1 = GetHeaderInfo().Substring(362, 14)
            SoftwareVersion = HeaderBytes(188).ToString()
            ComplementCheck = HeaderBytes(189).ToString()
            Reserved2 = GetHeaderInfo().Substring(380, 4)
        Else
            ' ヘッダーが不完全な場合の処理
            ' ...
        End If
    End Sub
End Class

' 子クラス（NDS用）
Public Class NdsRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "NDS_"
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' NDS固有のプロパティ
    Public Property GameTitle As String
    Public Property GameCode As String
    Public Property MakerCode As String
    Public Property UnitCode As String
    Public Property UnitCodeString As String
    Public Property EncryptionSeed As String
    Public Property CardSize As String
    Public Property CardSizeString As String
    Public Property MediaType As String
    Public Property Reserved3 As String
    Public Property RomVersion As String
    Public Property RegionCode As String
    Public Property RegionCodeString As String
    Public Property Autostart As String
    Public Property ARM9ROMOffset As String
    Public Property ARM9EntryAddress As String

    ' NDS固有の変換テーブル
    Private Shared ReadOnly NdsUnitCodes As New Dictionary(Of String, String) From {
        {"0", "Nintendo DS"}, {"2", "Nintendo DS Lite"}, {"3", "Nintendo DSi"}
    }
    Private Shared ReadOnly NdsRegionCodes As New Dictionary(Of String, String) From {
    {"0", "Normal"}, {"1", "China"}, {"2", "Korea"}, {"15", "All"}
}

    Private Shared ReadOnly NdsMediaTypes As New Dictionary(Of String, String) From {
    {"0", "Not Specified"}, {"1", "128kB EEPROM"}, {"2", "256kB EEPROM"}, {"3", "512kB EEPROM"}, {"4", "1MB EEPROM / 256kB FRAM"}, {"5", "2MB EEPROM"}, {"16", "256kB FLASH"}, {"32", "512kB FLASH"}, {"64", "1MB FLASH"}, {"128", "2MB FLASH"}
}

    Private Shared ReadOnly NdsCardSizes As New Dictionary(Of String, String) From {
    {"0", "5MB"}, {"1", "10MB"}, {"2", "20MB"}, {"3", "40MB"}, {"4", "80MB"}, {"5", "160MB"}, {"6", "320MB"}, {"7", "640MB"}
}

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 512 Then
            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 12).Trim()
            GameCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 12, 4)
            MakerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 16, 2)
            UnitCode = HeaderBytes(18).ToString()
            UnitCodeString = GetMappedValue(NdsUnitCodes, UnitCode)
            EncryptionSeed = HeaderBytes(19).ToString()
            CardSize = HeaderBytes(20).ToString()
            CardSizeString = GetMappedValue(NdsCardSizes, CardSize)
            MediaType = GetMappedValue(NdsMediaTypes, HeaderBytes(20).ToString())
            Reserved3 = GetHeaderInfo().Substring(42, 18)
            RomVersion = HeaderBytes(30).ToString()
            RegionCode = HeaderBytes(31).ToString()
            RegionCodeString = GetMappedValue(NdsRegionCodes, RegionCode)
            Autostart = HeaderBytes(32).ToString()
            ARM9ROMOffset = BitConverter.ToUInt32(HeaderBytes, 33).ToString()
            ARM9EntryAddress = BitConverter.ToUInt32(HeaderBytes, 37).ToString()
        Else
            ' ヘッダーが不完全な場合の処理
            ' ...
        End If
    End Sub
End Class

' 子クラス（3DS用）
Public Class N3dsRomHeader
    Inherits RomHeader

    Public Sub New()
        Prefix = "2DS_"
        HeaderSize = 512
        HeaderBytes = New Byte(HeaderSize - 1) {}
    End Sub

    ' 3DS固有のプロパティ
    Public Property GameTitle As String
    Public Property GameCode As String
    Public Property MakerCode As String
    Public Property Reserved4 As String
    Public Property MediaFormat As String
    Public Property MediaFormatString As String
    Public Property UnitCode As String
    Public Property SeedCheck As String
    Public Property MediaID As String
    Public Property RegionCode As String

    Public Property ProgramID As String

    ' 3DS固有の変換テーブル
    Private Shared ReadOnly N3dsMediaFormats As New Dictionary(Of String, String) From {
    {"0", "Inner Device"}, {"1", "Card1"}, {"2", "Card2"}, {"3", "Extended Device"}, {"15", "Not Specified"}
}

    Private Shared ReadOnly N3dsRegionCodes As New Dictionary(Of String, String) From {
    {"0", "Japan"}, {"1", "Americas"}, {"2", "Europe"}, {"4", "China"}, {"5", "Korea"}, {"6", "Taiwan"}
}

    Public Overrides Sub SetHeaderInfo()
        If HeaderBytes.Length >= 512 Then
            GameTitle = System.Text.Encoding.ASCII.GetString(HeaderBytes, 0, 12).Trim()
            GameCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 12, 4)
            MakerCode = System.Text.Encoding.ASCII.GetString(HeaderBytes, 16, 2)
            Reserved4 = GetHeaderInfo().Substring(36, 4)
            MediaFormat = HeaderBytes(20).ToString()
            MediaFormatString = GetMappedValue(N3dsMediaFormats, MediaFormat)
            UnitCode = HeaderBytes(21).ToString()
            SeedCheck = HeaderBytes(22).ToString()
            MediaID = HeaderBytes(23).ToString()
            RegionCode = GetMappedValue(N3dsRegionCodes, MediaID)
            ProgramID = GetHeaderInfo().Substring(48, 16)
        Else
            ' ヘッダーが不完全な場合の処理
            ' ...
        End If
    End Sub
End Class
