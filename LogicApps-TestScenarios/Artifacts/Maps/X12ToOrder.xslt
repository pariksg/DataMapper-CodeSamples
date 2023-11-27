<xsl:stylesheet xmlns:b="http://schemas.microsoft.com/BizTalk/2003" xmlns:ns0="http://Inbound_EDI.OrderFile" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:btsedi="http://schemas.microsoft.com/BizTalk/2005/EdiSchemaEditorExtension" xmlns:ns0_0="http://schemas.microsoft.com/BizTalk/EDI/X12/2006" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:math="http://www.w3.org/2005/xpath-functions/math" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dm="http://azure.workflow.datamapper" xmlns:ef="http://azure.workflow.datamapper.extensions" exclude-result-prefixes="xsl xs math dm ef btsedi b ns0_0" version="3.0" expand-text="yes">
  <xsl:output indent="yes" media-type="text/xml" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates select="." mode="azure.workflow.datamapper" />
  </xsl:template>
  <xsl:template match="/" mode="azure.workflow.datamapper">
    <ns0:OrderFile>
      <Order>
        <Header>
          <PODate>{/ns0_0:X12_00401_850/ns0_0:BEG/BEG05}</PODate>
          <PONumber>{/ns0_0:X12_00401_850/ns0_0:BEG/BEG03}</PONumber>
          <xsl:choose>
            <xsl:when test="exists(/ns0_0:X12_00401_850/ns0_0:BEG/BEG08)">
              <CustomerID>{/ns0_0:X12_00401_850/ns0_0:BEG/BEG08}</CustomerID>
            </xsl:when>
          </xsl:choose>
          <xsl:choose>
            <xsl:when test="(/ns0_0:X12_00401_850/ns0_0:PER/PER01) = ('AA')">
              <CustomerContactName>{/ns0_0:X12_00401_850/ns0_0:PER/PER02}</CustomerContactName>
            </xsl:when>
          </xsl:choose>
          <xsl:choose>
            <xsl:when test="(/ns0_0:X12_00401_850/ns0_0:PER/PER03) = ('TE')">
              <CustomerContactPhone>{/ns0_0:X12_00401_850/ns0_0:PER/PER04}</CustomerContactPhone>
            </xsl:when>
          </xsl:choose>
        </Header>
        <xsl:for-each select="/ns0_0:X12_00401_850/ns0_0:PO1Loop1">
          <LineItems>
            <PONumber>{/ns0_0:X12_00401_850/ns0_0:BEG/BEG03}</PONumber>
            <xsl:choose>
              <xsl:when test="exists(ns0_0:PO1/PO107)">
                <ItemOrdered>{ns0_0:PO1/PO107}</ItemOrdered>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="exists(ns0_0:PO1/PO102)">
                <Quantity>{ns0_0:PO1/PO102}</Quantity>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="exists(ns0_0:PO1/PO103)">
                <UOM>{ns0_0:PO1/PO103}</UOM>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="exists(ns0_0:PO1/PO104)">
                <Price>{ns0_0:PO1/PO104}</Price>
              </xsl:when>
            </xsl:choose>
            <ExtendedPrice>{(ns0_0:PO1/PO102) * (ns0_0:PO1/PO104)}</ExtendedPrice>
            <xsl:choose>
              <xsl:when test="exists(ns0_0:PIDLoop1/ns0_0:PID_2/PID05)">
                <Description>{ns0_0:PIDLoop1/ns0_0:PID_2/PID05}</Description>
              </xsl:when>
            </xsl:choose>
          </LineItems>
        </xsl:for-each>
      </Order>
    </ns0:OrderFile>
  </xsl:template>
</xsl:stylesheet>