<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:wix="http://wixtoolset.org/schemas/v4/wxs">
<xsl:output method="xml" indent="yes" cdata-section-elements="wix:Condition"/>
<xsl:strip-space elements="*" />

<xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
  </xsl:template>
  
<xsl:template match='wix:Component[@Id and (@Id="cmpEFAB68C7CD45D5422D0973F740293912")]'>
    <xsl:copy>
		<xsl:apply-templates select="@*|node()"/>
      	<xsl:element name="util:XmlFile" namespace="http://wixtoolset.org/schemas/v4/wxs/util">	
              <xsl:attribute name="Id">XmlHostName</xsl:attribute>
              <xsl:attribute name="Action">setValue</xsl:attribute>
              <xsl:attribute name="Permanent">yes</xsl:attribute>
              <xsl:attribute name="File">[#filFEDCE752CEEB47ED4DD897B32B91D60D]</xsl:attribute>
              <xsl:attribute name="ElementPath">//configuration/appSettings/add[\[]@key='HostName'[\]]</xsl:attribute>
              <xsl:attribute name="Value">[HOSTNAME]</xsl:attribute>
              <xsl:attribute name="Name">value</xsl:attribute>
              <xsl:attribute name="SelectionLanguage">XSLPattern</xsl:attribute>
              <xsl:attribute name="Sequence">1</xsl:attribute>  		
      	</xsl:element>
		<xsl:element name="util:XmlFile" namespace="http://wixtoolset.org/schemas/v4/wxs/util">	
              <xsl:attribute name="Id">XmlServerPort</xsl:attribute>
              <xsl:attribute name="Action">setValue</xsl:attribute>
              <xsl:attribute name="Permanent">yes</xsl:attribute>
              <xsl:attribute name="File">[#filFEDCE752CEEB47ED4DD897B32B91D60D]</xsl:attribute>
              <xsl:attribute name="ElementPath">//configuration/appSettings/add[\[]@key='ServerPort'[\]]</xsl:attribute>
              <xsl:attribute name="Value">[SERVERPORT]</xsl:attribute>
              <xsl:attribute name="Name">value</xsl:attribute>
              <xsl:attribute name="SelectionLanguage">XSLPattern</xsl:attribute>
              <xsl:attribute name="Sequence">1</xsl:attribute>  		
      	</xsl:element>
		<xsl:element name="util:XmlFile" namespace="http://wixtoolset.org/schemas/v4/wxs/util">	
              <xsl:attribute name="Id">XmlPFXThumbPrint</xsl:attribute>
              <xsl:attribute name="Action">setValue</xsl:attribute>
              <xsl:attribute name="Permanent">yes</xsl:attribute>
              <xsl:attribute name="File">[#filFEDCE752CEEB47ED4DD897B32B91D60D]</xsl:attribute>
              <xsl:attribute name="ElementPath">//configuration/appSettings/add[\[]@key='pfxthumbPrint'[\]]</xsl:attribute>
              <xsl:attribute name="Value">[PFXTHUMBPRINT]</xsl:attribute>
              <xsl:attribute name="Name">value</xsl:attribute>
              <xsl:attribute name="SelectionLanguage">XSLPattern</xsl:attribute>
              <xsl:attribute name="Sequence">1</xsl:attribute>  		
      	</xsl:element>		
		<xsl:element name="util:XmlFile" namespace="http://wixtoolset.org/schemas/v4/wxs/util">	
              <xsl:attribute name="Id">XmlSRC</xsl:attribute>
              <xsl:attribute name="Action">setValue</xsl:attribute>
              <xsl:attribute name="Permanent">yes</xsl:attribute>
              <xsl:attribute name="File">[#filFEDCE752CEEB47ED4DD897B32B91D60D]</xsl:attribute>
              <xsl:attribute name="ElementPath">//configuration/appSettings/add[\[]@key='SRC'[\]]</xsl:attribute>
              <xsl:attribute name="Value">[SRC]</xsl:attribute>
              <xsl:attribute name="Name">value</xsl:attribute>
              <xsl:attribute name="SelectionLanguage">XSLPattern</xsl:attribute>
              <xsl:attribute name="Sequence">1</xsl:attribute>  		
      	</xsl:element>	
		<xsl:element name="util:XmlFile" namespace="http://wixtoolset.org/schemas/v4/wxs/util">	
              <xsl:attribute name="Id">XmlIPAddressIndex</xsl:attribute>
              <xsl:attribute name="Action">setValue</xsl:attribute>
              <xsl:attribute name="Permanent">yes</xsl:attribute>
              <xsl:attribute name="File">[#filFEDCE752CEEB47ED4DD897B32B91D60D]</xsl:attribute>
              <xsl:attribute name="ElementPath">//configuration/appSettings/add[\[]@key='IPAddressIndex'[\]]</xsl:attribute>
              <xsl:attribute name="Value">[IPINDEX]</xsl:attribute>
              <xsl:attribute name="Name">value</xsl:attribute>
              <xsl:attribute name="SelectionLanguage">XSLPattern</xsl:attribute>
              <xsl:attribute name="Sequence">1</xsl:attribute>  		
      	</xsl:element>				
    </xsl:copy>
</xsl:template>

</xsl:stylesheet>