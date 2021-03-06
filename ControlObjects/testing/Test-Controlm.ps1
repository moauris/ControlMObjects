$dir = "..\bin\Debug\netstandard2.0\ControlM.dll"
# Run Add Type and add the types into the PS Instance

# Run Test to see if type file is available:
$dllFile = [System.IO.FileInfo]::new($dir)
"Does ControlM.dll File Exist?"
$dllFile.Exists
Add-Type -Path $dir

#Create Mockup Machines:
# A machine with no domain and ipv6
$Machine = [ControlM.ClientMachine]::new("Aur00101","192.168.0.101",
    [ControlM.OSInfo]::("Microsoft Windows", "Server 2008", "64-bit"))
$Node = [ControlM.ClientNode_Standalone]::new($Machine)
# A machine with domain and ipv6
$Machine1 = [ControlM.ClientMachine]::new(
    "Aur00201",
    ".chenmo.com.cn",
    "192.168.2.201",
    "fe80::9d72:8896:8498:201",
    [ControlM.OSInfo]::("Microsoft Windows", "Server 2016", "64-bit"))
$Node1 = [ControlM.ClientNode_Standalone]::new($Machine1)
# Three machines with domain and ipv6
$Machine2 = [ControlM.ClientMachine]::new(
    "Aur00300",
    ".chenmo.com.cn",
    "192.168.3.1",
    "fe80::9d72:8896:8493:1001",
    [ControlM.OSInfo]::new("Red Hat Linux Enterprise", "7.1", "64-bit"))
$Machine3 = [ControlM.ClientMachine]::new(
    "Aur00301",
    ".chenmo.com.cn",
    "192.168.3.2",
    "fe80::9d72:8896:8493:1002",
    [ControlM.OSInfo]::new("Red Hat Linux Enterprise", "6.4", "32-bit"))
$Machine4 = [ControlM.ClientMachine]::new(
    "Aur10302",
    ".chenmo.com.cn",
    "192.168.3.3",
    "fe80::9d72:8896:8493:1003",
    [ControlM.OSInfo]::("Microsoft Windows", "10", "64-bit"))
$Node2 = [ControlM.ClientNode_Cluster]::new($Machine2, $Machine3, $Machine4)

Write-Debug "Testing ToHostEntry Function in Node"
$Node.ToHostEntry("Comment")
$Node1.ToHostEntry("Comment")
$Node2.ToHostEntry("Comment")

#Create two MS Machine Clusters
$MSVer9 = [ControlM.ControlMVersion]::ctlmVer9018()
$MSVer7 = [ControlM.ControlMVersion]::ctlmVer7000fp5()
$Server1 = [ControlM.Server]::new(
        $MSVer9,
        [ControlM.ClientMachine]::new(
        "mst00101",".ctlm.com.cn","192.32.1.101","fe80::9d72:8896:ae86:1001",
        [ControlM.OSInfo]::new("Linux", "7.1", "64-bit")).ConvertToNode()
    )
$Server2 = [ControlM.Server]::new(
        $MSVer7,
        [ControlM.ClientMachine]::new(
        "mst00201",".ctlm.com.cn","192.32.1.201","fe80::9d72:8896:ae86:1002",
        [ControlM.OSInfo]::new("AIX", "7.2", "64-bit")).ConvertToNode()
    )
$Server1.Node.ToHostEntry("MServer 1")
$Server2.Node.ToHostEntry("MServer 2")
#Creating Agents
$Agent1 = [ControlM.Agent]::new($MSVer9, $Node, $Server1, 27006)
$Agent2 = [ControlM.Agent]::new($MSVer7, $Node1, $Server1, 27006)
$Agent3 = [ControlM.Agent]::new($MSVer9, $Node2, $Server1, 27006)

$Agent1.ToString()
"$($Agent1.Version)"
"$($Agent1.OSInformation)"
$Agent2.ToString()
"$($Agent2.Version)"
"$($Agent2.OSInformation)"
$Agent3.ToString()
"$($Agent3.Version)"
"$($Agent3.OSInformation)"