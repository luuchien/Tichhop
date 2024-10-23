<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="dangkynghi">
		<html>
			<head>
				<style>
					.tr{
						background-color : green;
					}
				</style>
			</head>
			<body>
				<h1 align ="center">Bang</h1>
				<xsl:for-each select="ngaylamviec">
					<h2>Ngay lam viec : <xsl:value-of select="@ngay"/>
				</h2>
					<table border ="1">
						<tr class="tr">
							<th>STT</th>
							<th>Ma NV</th>
							<th>Ly do</th>
							<th>Don Vi</th>
							<th>Trang thai</th>
						</tr>
						<xsl:for-each select="nhanvien">
							<tr>
								<td>
									<xsl:value-of select="position()"/>
								</td>
								<td>
									<xsl:value-of select="@manv"/>
								</td>
								<td>
									<xsl:value-of select="lydo"/>
								</td>
								<td>
									<xsl:value-of select="donvi"/>
								</td>
								<td>
									<xsl:value-of select="trangthai"/>
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</xsl:for-each>
			</body>
		</html>
    </xsl:template>
</xsl:stylesheet>
