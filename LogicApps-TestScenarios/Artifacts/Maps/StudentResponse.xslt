<xsl:stylesheet xmlns:ns0="http://StudentEnrollment.StudentResponse" xmlns:b="http://schemas.microsoft.com/BizTalk/2003" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:ns0_0="http://StudentEnrollment.Student" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:math="http://www.w3.org/2005/xpath-functions/math" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dm="http://azure.workflow.datamapper" xmlns:ef="http://azure.workflow.datamapper.extensions" exclude-result-prefixes="xsl xs math dm ef ns0_0 b" version="3.0" expand-text="yes">
  <xsl:output indent="yes" media-type="text/xml" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates select="." mode="azure.workflow.datamapper" />
  </xsl:template>
  <xsl:template match="/" mode="azure.workflow.datamapper">
    <ns0:StudentResponse>
      <Name>{normalize-space(concat(upper-case(/Q{http://StudentEnrollment.Student}Student/LastName), ', ', /Q{http://StudentEnrollment.Student}Student/FirstName, ' ', /Q{http://StudentEnrollment.Student}Student/MiddleInitial))}</Name>
      <DateOfBirth>{/ns0_0:Student/DateOfBirth}</DateOfBirth>
      <Age>{ef:age(/ns0_0:Student/DateOfBirth)}</Age>
      <MailingAddress>{concat(/Q{http://StudentEnrollment.Student}Student/Address/StreetAddress, ' ', /Q{http://StudentEnrollment.Student}Student/Address/City, ' ', /Q{http://StudentEnrollment.Student}Student/Address/Zip, ' ', upper-case(/Q{http://StudentEnrollment.Student}Student/Address/State))}</MailingAddress>
      <Phone>
        <ContactInfo xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:st="http://StudentEnrollment.Student">
  <xsl:attribute name="ContactType">
    <xsl:value-of select="/st:Student/Address/ContactType" />
  </xsl:attribute>
  <xsl:attribute name="Contact">
    <xsl:value-of select="/st:Student/Address/Contact" />
  </xsl:attribute>
</ContactInfo>
</Phone>
    </ns0:StudentResponse>
  </xsl:template>
  <xsl:function name="ef:age" as="xs:float">
    <xsl:param name="inputDate" as="xs:date" />
    <xsl:value-of select="round(days-from-duration(current-date() - xs:date($inputDate)) div 365.25, 1)" />
  </xsl:function>
</xsl:stylesheet>