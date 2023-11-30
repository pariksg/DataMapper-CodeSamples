<xsl:stylesheet xmlns:btsedi="http://schemas.microsoft.com/BizTalk/2005/EdiSchemaEditorExtension" xmlns:b="http://schemas.microsoft.com/BizTalk/2003" xmlns:ns0="http://schemas.microsoft.com/BizTalk/EDI/X12/2006" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:math="http://www.w3.org/2005/xpath-functions/math" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dm="http://azure.workflow.datamapper" xmlns:ef="http://azure.workflow.datamapper.extensions" xmlns="http://www.w3.org/2005/xpath-functions" exclude-result-prefixes="xsl xs math dm ef btsedi b ns0" version="3.0" expand-text="yes">
  <xsl:output indent="yes" media-type="text/json" method="text" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <xsl:variable name="xmloutput">
      <xsl:apply-templates select="." mode="azure.workflow.datamapper" />
    </xsl:variable>
    <xsl:value-of select="xml-to-json($xmloutput,map{'indent':true()})" />
  </xsl:template>
  <xsl:template match="/" mode="azure.workflow.datamapper">
    <map>
      <map key="OrderFile">
        <map key="Order">
          <map key="Header">
            <string key="PODate">{/ns0:X12_00401_850/ns0:BEG/BEG05}</string>
            <string key="PONumber">{/ns0:X12_00401_850/ns0:BEG/BEG03}</string>
            <xsl:choose>
              <xsl:when test="exists(/ns0:X12_00401_850/ns0:BEG/BEG08)">
                <string key="CustomerID">{/ns0:X12_00401_850/ns0:BEG/BEG08}</string>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="(/ns0:X12_00401_850/ns0:PER/PER01) = ('AA')">
                <string key="CustomerContactName">{/ns0:X12_00401_850/ns0:PER/PER02}</string>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="(/ns0:X12_00401_850/ns0:PER/PER03) = ('TE')">
                <string key="CustomerContactPhone">{/ns0:X12_00401_850/ns0:PER/PER04}</string>
              </xsl:when>
            </xsl:choose>
          </map>
          <array key="LineItems">
            <xsl:for-each select="/ns0:X12_00401_850/ns0:PO1Loop1">
              <map>
                <string key="PONumber">{/ns0:X12_00401_850/ns0:BEG/BEG03}</string>
                <xsl:choose>
                  <xsl:when test="exists(ns0:PO1/PO107)">
                    <string key="ItemOrdered">{ns0:PO1/PO107}</string>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="exists(ns0:PO1/PO102)">
                    <number key="Quantity">{ns0:PO1/PO102}</number>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="exists(ns0:PO1/PO103)">
                    <string key="UOM">{ns0:PO1/PO103}</string>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="exists(ns0:PO1/PO104)">
                    <number key="Price">{ns0:PO1/PO104}</number>
                  </xsl:when>
                </xsl:choose>
                <number key="ExtendedPrice">{(ns0:PO1/PO102) * (ns0:PO1/PO104)}</number>
                <xsl:choose>
                  <xsl:when test="exists(ns0:PIDLoop1/ns0:PID_2/PID05)">
                    <string key="Description">{ns0:PIDLoop1/ns0:PID_2/PID05}</string>
                  </xsl:when>
                </xsl:choose>
              </map>
            </xsl:for-each>
          </array>
        </map>
      </map>
    </map>
  </xsl:template>
</xsl:stylesheet>