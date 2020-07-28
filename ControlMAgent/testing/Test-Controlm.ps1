$dir = "..\bin\Debug\netstandard2.0\ControlM.dll"
# Run Add Type and add the types into the PS Instance
Add-Type -Path $dir

#Create Mockup Machines:
# A machine with no domain and ipv6
$Machine = [ControlM.ClientMachine]::new("Aur00101","192.168.0.101")
$Node = [ControlM.ClientNode_Standalone]::new($Machine)
# A machine with domain and ipv6
$Machine1 = [ControlM.ClientMachine]::new(
    "Aur00201",
    ".chenmo.com.cn",
    "192.168.2.201",
    "fe80::9d72:8896:8498:201")
$Node1 = [ControlM.ClientNode_Standalone]::new($Machine1)
# Three machines with domain and ipv6
$Machine2 = [ControlM.ClientMachine]::new(
    "Aur00300",
    ".chenmo.com.cn",
    "192.168.3.1",
    "fe80::9d72:8896:8493:1001")
$Machine3 = [ControlM.ClientMachine]::new(
    "Aur00301",
    ".chenmo.com.cn",
    "192.168.3.2",
    "fe80::9d72:8896:8493:1002")
$Machine4 = [ControlM.ClientMachine]::new(
    "Aur10302",
    ".chenmo.com.cn",
    "192.168.3.3",
    "fe80::9d72:8896:8493:1003")
$Node2 = [ControlM.ClientNode_Cluster]::new($Machine2, $Machine3, $Machine4)

Write-Debug "Testing ToHostEntry Function in Node"
$Node.ToHostEntry("Comment")
$Node1.ToHostEntry("Comment")
$Node2.ToHostEntry("Comment")

#Create two MS Machine Clusters
$MSVer9 = [ControlM.ControlMVersion]::New([ControlM.VersionOption]::v9018)
$MSVer7 = [ControlM.ControlMVersion]::New([ControlM.VersionOption]::v7000fp5)
$Server1 = [ControlM.Server]::New(
        $MSVer9,
        [ControlM.ClientMachine]::new(
        "mst00101",".ctlm.com.cn","192.32.1.101","fe80::9d72:8896:ae86:1001").ConvertToNode()
    )
$Server2 = [ControlM.Server]::New(
        $MSVer7,
        [ControlM.ClientMachine]::new(
        "mst00201",".ctlm.com.cn","192.32.1.201","fe80::9d72:8896:ae86:1002").ConvertToNode()
    )
$Server1.Node.ToHostEntry("MServer 1")
$Server2.Node.ToHostEntry("MServer 2")
#Creating Agents
$Agent1 = [ControlM.Agent]::new($MSVer9, $Node, $Server1, 27006)
$Agent2 = [ControlM.Agent]::new($MSVer7, $Node1, $Server1, 27006)
$Agent3 = [ControlM.Agent]::new($MSVer9, $Node2, $Server1, 27006)

$Agent1.ToString()
$Agent2.ToString()
$Agent3.ToString()