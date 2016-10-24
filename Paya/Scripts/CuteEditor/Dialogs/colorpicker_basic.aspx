<%@ Page Language="C#" Inherits="CuteEditor.EditorUtilityPage" %>
<script runat="server">
string GetDialogQueryString;
override protected void OnInit(EventArgs args)
{
	if(Context.Request.QueryString["Dialog"]=="Standard")
	{	
	if(Context.Request.QueryString["IsFrame"]==null)
	{
		string FrameSrc="colorpicker_basic.aspx?IsFrame=1&"+Request.ServerVariables["QUERY_STRING"];
		CuteEditor.CEU.WriteDialogOuterFrame(Context,"[[MoreColors]]",FrameSrc);
		Context.Response.End();
	}
	}
	string s="";
	if(Context.Request.QueryString["Dialog"]=="Standard")	
		s="&Dialog=Standard";
	
	GetDialogQueryString="Theme="+Context.Request.QueryString["Theme"]+s;	
	base.OnInit(args);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
		<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
		<script type="text/javascript" src="Load.ashx?type=dialogscript&file=DialogHead.js"></script>
		<script type="text/javascript" src="Load.ashx?type=dialogscript&file=Dialog_ColorPicker.js"></script>
		<link href='Load.ashx?type=themecss&file=dialog.css&theme=[[_Theme_]]' type="text/css"
			rel="stylesheet" />
		<style type="text/css">
			.colorcell
			{
				width:16px;
				height:17px;
				cursor:hand;
			}
			.colordiv,.customdiv
			{
				border:solid 1px #808080;
				width:16px;
				height:17px;
				font-size:1px;
			}
		</style>
		<title>[[NamedColors]]</title>
		<script>
								
		var OxOf1a6=["Green","#008000","Lime","#00FF00","Teal","#008080","Aqua","#00FFFF","Navy","#000080","Blue","#0000FF","Purple","#800080","Fuchsia","#FF00FF","Maroon","#800000","Red ","#FF0000","Olive","#808000","Yellow","#FFFF00","White","#FFFFFF","Silver","#C0C0C0","Gray","#808080","Black","#000000","DarkOliveGreen","#556B2F","DarkGreen","#006400","DarkSlateGray","#2F4F4F","SlateGray","#708090","DarkBlue","#00008B","MidnightBlue","#191970","Indigo","#4B0082","DarkMagenta","#8B008B","Brown","#A52A2A","DarkRed","#8B0000","Sienna","#A0522D","SaddleBrown","#8B4513","DarkGoldenrod","#B8860B","Beige","#F5F5DC","HoneyDew","#F0FFF0","DimGray","#696969","OliveDrab","#6B8E23","ForestGreen","#228B22","DarkCyan","#008B8B","LightSlateGray","#778899","MediumBlue","#0000CD","DarkSlateBlue","#483D8B","DarkViolet","#9400D3","MediumVioletRed","#C71585","IndianRed","#CD5C5C","Firebrick","#B22222","Chocolate","#D2691E","Peru","#CD853F","Goldenrod","#DAA520","LightGoldenrodYellow","#FAFAD2","MintCream","#F5FFFA","DarkGray","#A9A9A9","YellowGreen","#9ACD32","SeaGreen","#2E8B57","CadetBlue","#5F9EA0","SteelBlue","#4682B4","RoyalBlue","#4169E1","BlueViolet","#8A2BE2","DarkOrchid","#9932CC","DeepPink","#FF1493","RosyBrown","#BC8F8F","Crimson","#DC143C","DarkOrange","#FF8C00","BurlyWood","#DEB887","DarkKhaki","#BDB76B","LightYellow","#FFFFE0","Azure","#F0FFFF","LightGray","#D3D3D3","LawnGreen","#7CFC00","MediumSeaGreen","#3CB371","LightSeaGreen","#20B2AA","DeepSkyBlue","#00BFFF","DodgerBlue","#1E90FF","SlateBlue","#6A5ACD","MediumOrchid","#BA55D3","PaleVioletRed","#DB7093","Salmon","#FA8072","OrangeRed","#FF4500","SandyBrown","#F4A460","Tan","#D2B48C","Gold","#FFD700","Ivory","#FFFFF0","GhostWhite","#F8F8FF","Gainsboro","#DCDCDC","Chartreuse","#7FFF00","LimeGreen","#32CD32","MediumAquamarine","#66CDAA","DarkTurquoise","#00CED1","CornflowerBlue","#6495ED","MediumSlateBlue","#7B68EE","Orchid","#DA70D6","HotPink","#FF69B4","LightCoral","#F08080","Tomato","#FF6347","Orange","#FFA500","Bisque","#FFE4C4","Khaki","#F0E68C","Cornsilk","#FFF8DC","Linen","#FAF0E6","WhiteSmoke","#F5F5F5","GreenYellow","#ADFF2F","DarkSeaGreen","#8FBC8B","Turquoise","#40E0D0","MediumTurquoise","#48D1CC","SkyBlue","#87CEEB","MediumPurple","#9370DB","Violet","#EE82EE","LightPink","#FFB6C1","DarkSalmon","#E9967A","Coral","#FF7F50","NavajoWhite","#FFDEAD","BlanchedAlmond","#FFEBCD","PaleGoldenrod","#EEE8AA","Oldlace","#FDF5E6","Seashell","#FFF5EE","PaleGreen","#98FB98","SpringGreen","#00FF7F","Aquamarine","#7FFFD4","PowderBlue","#B0E0E6","LightSkyBlue","#87CEFA","LightSteelBlue","#B0C4DE","Plum","#DDA0DD","Pink","#FFC0CB","LightSalmon","#FFA07A","Wheat","#F5DEB3","Moccasin","#FFE4B5","AntiqueWhite","#FAEBD7","LemonChiffon","#FFFACD","FloralWhite","#FFFAF0","Snow","#FFFAFA","AliceBlue","#F0F8FF","LightGreen","#90EE90","MediumSpringGreen","#00FA9A","PaleTurquoise","#AFEEEE","LightCyan","#E0FFFF","LightBlue","#ADD8E6","Lavender","#E6E6FA","Thistle","#D8BFD8","MistyRose","#FFE4E1","Peachpuff","#FFDAB9","PapayaWhip","#FFEFD5"];var colorlist=[{n:OxOf1a6[0],h:OxOf1a6[1]},{n:OxOf1a6[2],h:OxOf1a6[3]},{n:OxOf1a6[4],h:OxOf1a6[5]},{n:OxOf1a6[6],h:OxOf1a6[7]},{n:OxOf1a6[8],h:OxOf1a6[9]},{n:OxOf1a6[10],h:OxOf1a6[11]},{n:OxOf1a6[12],h:OxOf1a6[13]},{n:OxOf1a6[14],h:OxOf1a6[15]},{n:OxOf1a6[16],h:OxOf1a6[17]},{n:OxOf1a6[18],h:OxOf1a6[19]},{n:OxOf1a6[20],h:OxOf1a6[21]},{n:OxOf1a6[22],h:OxOf1a6[23]},{n:OxOf1a6[24],h:OxOf1a6[25]},{n:OxOf1a6[26],h:OxOf1a6[27]},{n:OxOf1a6[28],h:OxOf1a6[29]},{n:OxOf1a6[30],h:OxOf1a6[31]}];var colormore=[{n:OxOf1a6[32],h:OxOf1a6[33]},{n:OxOf1a6[34],h:OxOf1a6[35]},{n:OxOf1a6[36],h:OxOf1a6[37]},{n:OxOf1a6[38],h:OxOf1a6[39]},{n:OxOf1a6[40],h:OxOf1a6[41]},{n:OxOf1a6[42],h:OxOf1a6[43]},{n:OxOf1a6[44],h:OxOf1a6[45]},{n:OxOf1a6[46],h:OxOf1a6[47]},{n:OxOf1a6[48],h:OxOf1a6[49]},{n:OxOf1a6[50],h:OxOf1a6[51]},{n:OxOf1a6[52],h:OxOf1a6[53]},{n:OxOf1a6[54],h:OxOf1a6[55]},{n:OxOf1a6[56],h:OxOf1a6[57]},{n:OxOf1a6[58],h:OxOf1a6[59]},{n:OxOf1a6[60],h:OxOf1a6[61]},{n:OxOf1a6[62],h:OxOf1a6[63]},{n:OxOf1a6[64],h:OxOf1a6[65]},{n:OxOf1a6[66],h:OxOf1a6[67]},{n:OxOf1a6[68],h:OxOf1a6[69]},{n:OxOf1a6[70],h:OxOf1a6[71]},{n:OxOf1a6[72],h:OxOf1a6[73]},{n:OxOf1a6[74],h:OxOf1a6[75]},{n:OxOf1a6[76],h:OxOf1a6[77]},{n:OxOf1a6[78],h:OxOf1a6[79]},{n:OxOf1a6[80],h:OxOf1a6[81]},{n:OxOf1a6[82],h:OxOf1a6[83]},{n:OxOf1a6[84],h:OxOf1a6[85]},{n:OxOf1a6[86],h:OxOf1a6[87]},{n:OxOf1a6[88],h:OxOf1a6[89]},{n:OxOf1a6[90],h:OxOf1a6[91]},{n:OxOf1a6[92],h:OxOf1a6[93]},{n:OxOf1a6[94],h:OxOf1a6[95]},{n:OxOf1a6[96],h:OxOf1a6[97]},{n:OxOf1a6[98],h:OxOf1a6[99]},{n:OxOf1a6[100],h:OxOf1a6[101]},{n:OxOf1a6[102],h:OxOf1a6[103]},{n:OxOf1a6[104],h:OxOf1a6[105]},{n:OxOf1a6[106],h:OxOf1a6[107]},{n:OxOf1a6[108],h:OxOf1a6[109]},{n:OxOf1a6[110],h:OxOf1a6[111]},{n:OxOf1a6[112],h:OxOf1a6[113]},{n:OxOf1a6[114],h:OxOf1a6[115]},{n:OxOf1a6[116],h:OxOf1a6[117]},{n:OxOf1a6[118],h:OxOf1a6[119]},{n:OxOf1a6[120],h:OxOf1a6[121]},{n:OxOf1a6[122],h:OxOf1a6[123]},{n:OxOf1a6[124],h:OxOf1a6[125]},{n:OxOf1a6[126],h:OxOf1a6[127]},{n:OxOf1a6[128],h:OxOf1a6[129]},{n:OxOf1a6[130],h:OxOf1a6[131]},{n:OxOf1a6[132],h:OxOf1a6[133]},{n:OxOf1a6[134],h:OxOf1a6[135]},{n:OxOf1a6[136],h:OxOf1a6[137]},{n:OxOf1a6[138],h:OxOf1a6[139]},{n:OxOf1a6[140],h:OxOf1a6[141]},{n:OxOf1a6[142],h:OxOf1a6[143]},{n:OxOf1a6[144],h:OxOf1a6[145]},{n:OxOf1a6[146],h:OxOf1a6[147]},{n:OxOf1a6[148],h:OxOf1a6[149]},{n:OxOf1a6[150],h:OxOf1a6[151]},{n:OxOf1a6[152],h:OxOf1a6[153]},{n:OxOf1a6[154],h:OxOf1a6[155]},{n:OxOf1a6[156],h:OxOf1a6[157]},{n:OxOf1a6[158],h:OxOf1a6[159]},{n:OxOf1a6[160],h:OxOf1a6[161]},{n:OxOf1a6[162],h:OxOf1a6[163]},{n:OxOf1a6[164],h:OxOf1a6[165]},{n:OxOf1a6[166],h:OxOf1a6[167]},{n:OxOf1a6[168],h:OxOf1a6[169]},{n:OxOf1a6[170],h:OxOf1a6[171]},{n:OxOf1a6[172],h:OxOf1a6[173]},{n:OxOf1a6[174],h:OxOf1a6[175]},{n:OxOf1a6[176],h:OxOf1a6[177]},{n:OxOf1a6[178],h:OxOf1a6[179]},{n:OxOf1a6[180],h:OxOf1a6[181]},{n:OxOf1a6[182],h:OxOf1a6[183]},{n:OxOf1a6[184],h:OxOf1a6[185]},{n:OxOf1a6[186],h:OxOf1a6[187]},{n:OxOf1a6[188],h:OxOf1a6[189]},{n:OxOf1a6[190],h:OxOf1a6[191]},{n:OxOf1a6[192],h:OxOf1a6[193]},{n:OxOf1a6[194],h:OxOf1a6[195]},{n:OxOf1a6[196],h:OxOf1a6[197]},{n:OxOf1a6[198],h:OxOf1a6[199]},{n:OxOf1a6[200],h:OxOf1a6[201]},{n:OxOf1a6[202],h:OxOf1a6[203]},{n:OxOf1a6[204],h:OxOf1a6[205]},{n:OxOf1a6[206],h:OxOf1a6[207]},{n:OxOf1a6[208],h:OxOf1a6[209]},{n:OxOf1a6[210],h:OxOf1a6[211]},{n:OxOf1a6[212],h:OxOf1a6[213]},{n:OxOf1a6[214],h:OxOf1a6[215]},{n:OxOf1a6[216],h:OxOf1a6[217]},{n:OxOf1a6[218],h:OxOf1a6[219]},{n:OxOf1a6[220],h:OxOf1a6[221]},{n:OxOf1a6[156],h:OxOf1a6[157]},{n:OxOf1a6[222],h:OxOf1a6[223]},{n:OxOf1a6[224],h:OxOf1a6[225]},{n:OxOf1a6[226],h:OxOf1a6[227]},{n:OxOf1a6[228],h:OxOf1a6[229]},{n:OxOf1a6[230],h:OxOf1a6[231]},{n:OxOf1a6[232],h:OxOf1a6[233]},{n:OxOf1a6[234],h:OxOf1a6[235]},{n:OxOf1a6[236],h:OxOf1a6[237]},{n:OxOf1a6[238],h:OxOf1a6[239]},{n:OxOf1a6[240],h:OxOf1a6[241]},{n:OxOf1a6[242],h:OxOf1a6[243]},{n:OxOf1a6[244],h:OxOf1a6[245]},{n:OxOf1a6[246],h:OxOf1a6[247]},{n:OxOf1a6[248],h:OxOf1a6[249]},{n:OxOf1a6[250],h:OxOf1a6[251]},{n:OxOf1a6[252],h:OxOf1a6[253]},{n:OxOf1a6[254],h:OxOf1a6[255]},{n:OxOf1a6[256],h:OxOf1a6[257]},{n:OxOf1a6[258],h:OxOf1a6[259]},{n:OxOf1a6[260],h:OxOf1a6[261]},{n:OxOf1a6[262],h:OxOf1a6[263]},{n:OxOf1a6[264],h:OxOf1a6[265]},{n:OxOf1a6[266],h:OxOf1a6[267]},{n:OxOf1a6[268],h:OxOf1a6[269]},{n:OxOf1a6[270],h:OxOf1a6[271]},{n:OxOf1a6[272],h:OxOf1a6[273]}];
		
		</script>
	</head>
	<body>
		<div id="container">
			<div class="tab-pane-control tab-pane" id="tabPane1">
				<div class="tab-row">
					<h2 class="tab">
						<a tabindex="-1" href='colorpicker.aspx?<%=GetDialogQueryString%>'>
							<span style="white-space:nowrap;">
								[[WebPalette]]
							</span>
						</a>
					</h2>
					<h2 class="tab selected">
							<a tabindex="-1" href='colorpicker_basic.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[NamedColors]]
								</span>
							</a>
					</h2>
					<h2 class="tab">
							<a tabindex="-1" href='colorpicker_more.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[CustomColor]]
								</span>
							</a>
					</h2>
				</div>
				<div class="tab-page">			
					<table class="colortable" align="center">
						<tr>
							<td colspan="16" height="16"><p align="left">Basic:
								</p>
							</td>
						</tr>
						<tr>
							<script>
								var OxO9449=["length","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E",""];var arr=[];for(var i=0;i<colorlist[OxO9449[0]];i++){arr.push(OxO9449[1]);arr.push(colorlist[i].n);arr.push(OxO9449[2]);arr.push(colorlist[i].n);arr.push(OxO9449[3]);arr.push(colorlist[i].h);arr.push(OxO9449[4]);arr.push(colorlist[i].n);arr.push(OxO9449[5]);arr.push(colorlist[i].h);arr.push(OxO9449[6]);} ;document.write(arr.join(OxO9449[7]));
							</script>
						</tr>
						<tr>
							<td colspan="16" height="12"><p align="left"></p>
							</td>
						</tr>
						<tr>
							<td colspan="16"><p align="left">Additional:
								</p>
							</td>
						</tr>
						<script>
							var OxO6070=["length","\x3Ctr\x3E","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E","\x3C/tr\x3E",""];var arr=[];for(var i=0;i<colormore[OxO6070[0]];i++){if(i%16==0){arr.push(OxO6070[1]);} ;arr.push(OxO6070[2]);arr.push(colormore[i].n);arr.push(OxO6070[3]);arr.push(colormore[i].n);arr.push(OxO6070[4]);arr.push(colormore[i].h);arr.push(OxO6070[5]);arr.push(colormore[i].n);arr.push(OxO6070[6]);arr.push(colormore[i].h);arr.push(OxO6070[7]);if(i%16==15){arr.push(OxO6070[8]);} ;} ;if(colormore%16>0){arr.push(OxO6070[8]);} ;document.write(arr.join(OxO6070[9]));
						</script>
						<tr>
							<td colspan="16" height="8">
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
								<input checked id="CheckboxColorNames" style="width: 16px; height: 20px" type="checkbox">
								<span style="width: 118px;">Use color names</span>
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
							</td>
						</tr>
						<tr>
							<td colspan="16" valign="middle" height="24">
							<span style="height:24px;width:50px;vertical-align:middle;">Color : </span>&nbsp;
							<input type="text" id="divpreview" size="7" maxlength="7" style="width:180px;height:24px;border:#a0a0a0 1px solid; Padding:4;"/>
					
							</td>
						</tr>
				</table>
			</div>
		</div>
		<div id="container-bottom">
			<input type="button" id="buttonok" value="[[OK]]" class="formbutton" style="width:70px"	onclick="do_insert();" /> 
			&nbsp;&nbsp;&nbsp;&nbsp; 
			<input type="button" id="buttoncancel" value="[[Cancel]]" class="formbutton" style="width:70px"	onclick="do_Close();" />	
		</div>
	</div>
	</body>
</html>

