<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="RdChat" generation="1" functional="0" release="0" Id="f8cd1ede-5fa0-4596-8618-50ebbe8de5aa" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="RdChatGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="RdChat_WebRole:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/RdChat/RdChatGroup/LB:RdChat_WebRole:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="RdChat_WebRole:ContainerName" defaultValue="">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapRdChat_WebRole:ContainerName" />
          </maps>
        </aCS>
        <aCS name="RdChat_WebRole:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapRdChat_WebRole:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="RdChat_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapRdChat_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="RdChat_WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapRdChat_WebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapWorkerRole1:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapWorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/RdChat/RdChatGroup/MapWorkerRole1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:RdChat_WebRole:HttpIn">
          <toPorts>
            <inPortMoniker name="/RdChat/RdChatGroup/RdChat_WebRole/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapRdChat_WebRole:ContainerName" kind="Identity">
          <setting>
            <aCSMoniker name="/RdChat/RdChatGroup/RdChat_WebRole/ContainerName" />
          </setting>
        </map>
        <map name="MapRdChat_WebRole:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/RdChat/RdChatGroup/RdChat_WebRole/DataConnectionString" />
          </setting>
        </map>
        <map name="MapRdChat_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/RdChat/RdChatGroup/RdChat_WebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapRdChat_WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/RdChat/RdChatGroup/RdChat_WebRoleInstances" />
          </setting>
        </map>
        <map name="MapWorkerRole1:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/RdChat/RdChatGroup/WorkerRole1/DataConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/RdChat/RdChatGroup/WorkerRole1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/RdChat/RdChatGroup/WorkerRole1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="RdChat_WebRole" generation="1" functional="0" release="0" software="C:\Users\mcaravajal\Desktop\WATK\Ex1-WorkingWithTables\Begin\RdChat\csx\Debug\roles\RdChat_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="ContainerName" defaultValue="" />
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;RdChat_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;RdChat_WebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/RdChat/RdChatGroup/RdChat_WebRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/RdChat/RdChatGroup/RdChat_WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRole1" generation="1" functional="0" release="0" software="C:\Users\mcaravajal\Desktop\WATK\Ex1-WorkingWithTables\Begin\RdChat\csx\Debug\roles\WorkerRole1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;RdChat_WebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/RdChat/RdChatGroup/WorkerRole1Instances" />
            <sCSPolicyFaultDomainMoniker name="/RdChat/RdChatGroup/WorkerRole1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="RdChat_WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="RdChat_WebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="eb5b69b3-f904-4cf9-b3f4-3758e2e8981f" ref="Microsoft.RedDog.Contract\ServiceContract\RdChatContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="e73c460d-2169-465b-a152-ec60754773f2" ref="Microsoft.RedDog.Contract\Interface\RdChat_WebRole:HttpIn@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/RdChat/RdChatGroup/RdChat_WebRole:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>