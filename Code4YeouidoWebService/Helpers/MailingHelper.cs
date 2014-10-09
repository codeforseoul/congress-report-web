using Code4YeouidoWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code4YeouidoWebService.Helpers
{
    public class MailingHelper
    {
        /// <summary>
        /// 한글날 기념 메서드 네이밍
        /// </summary>
        public static void 메일발송()
        {
            using (Code4YeouidoEntities db = new Code4YeouidoEntities())
            {
                //메일을 막 보냅니다.

                List<MailList> list = db.MailList.ToList();

                //삶이란 때때로 무서우므로 막 보내지 않습니다.
                list = list.Where(e => e.Email.Equals("chunsun21@nate.com") || e.Email.Equals("ed.kim@edkim.co")).ToList();

                foreach (var i in list)
                {
                    MP mp = db.MP.First(e => e.DistrictCompare.Equals(i.DistrictCompare));

                    double? ratio = db.GetAttendanceRate(mp.Party, mp.NameKr).First();
                    double? ratio_top = db.GetAttendancePercentTop(mp.Party, mp.NameKr).First();
                    int bill_count = db.Database.SqlQuery<int>("SELECT TOP 1000 COUNT([Title]) FROM [dbo].[BillList] WHERE (BillId IN (SELECT BillId FROM [dbo].BillChoice  WHERE PARTY = N'" + mp.Party + "' AND NAME = N'" + mp.NameKr + "' AND Choice NOT IN ('청가', '결석', '불참')))", new object[] { }).First();
                    var bill_qry = db.GetBillList(mp.Party, mp.NameKr).ToList();

                    //여기서부터는 충환 님께 부탁합니다.



                    //실제 메일 발송 <지민 작성 예정>
                }

                /*$email = 'chunsun21@nate.com';
$user_info = sqlsrv_fetch_array(sqlsrv_query($connect,"SELECT * FROM MailList WHERE Email = '".$email."'"));

$selector_info = sqlsrv_fetch_array(sqlsrv_query($connect,"SELECT * FROM MP WHERE DistrictCompare = '".$user_info['DistrictCompare']."'"));

$ratio = sqlsrv_fetch_array(sqlsrv_query($connect,"EXEC [dbo].[GetAttendanceRate] @Party = N'".$selector_info['Party']."',@Name = N'".$selector_info['NameKr']."'"));

$ratio_top = sqlsrv_fetch_array(sqlsrv_query($connect,"EXEC [dbo].[GetAttendancePercentTop] @Party = N'".$selector_info['Party']."',@Name = N'".$selector_info['NameKr']."'"));

$ratio_top = $ratio_top[0];

$bill_count = sqlsrv_fetch_array(sqlsrv_query($connect,"SELECT TOP 1000 COUNT([Title]) FROM [Code4Yeouido].[dbo].[BillList] WHERE (BillId IN (SELECT BillId FROM [Code4Yeouido].[dbo].BillChoice  WHERE PARTY = N'".$selector_info['Party']."' AND NAME = N'".$selector_info['NameKr']."' AND Choice NOT IN ('청가', '결석', '불참')))"));

$bill_qry = sqlsrv_query($connect,"EXEC	[dbo].[GetBillList] @Party = N'".$selector_info['Party']."', @Name = N'".$selector_info['NameKr']."'");

$i=0;
while($bill_rows = sqlsrv_fetch_array($bill_qry)){
	$bill[$i]['title'] = $bill_rows['Title'];
	$bill[$i]['number'] = $bill_rows['Number'];
	$bill[$i]['whenopen'] = $bill_rows['WhenOpen'];
	$bill[$i]['choice'] = $bill_rows['Choice'];
	$i++;
}

for($i=0;$i<18;$i++){
	$bill_row = $bill[$i];
	if($i < 9){
		$bill_status = "계류";
	} else {
		$bill_status = $bill_row['choice'];
	}
	$txt = "<tr>
					<td style='width:100px;font-size:12.34px;vertical-align:middle;text-align:center;color:#8a8a8a;height:30px;'>".$bill_row['whenopen']."</td>
					<td style='font-size:13.5px;vertical-align:middle;text-align:left;color:#525252;'>".$bill_row['title']."</td>
					<td style='width:85px;font-size:14.56px;vertical-align:middle;text-align:center;color:#7d7d7d;'>".$bill_status."</td>
			</tr>";
	if($i < 9){
		$tr1 .= $txt;
	} else {
		$tr2 .= $txt;
	}
}

if($ratio_top <= 4) {
	$top_txt = '본 의원은 매우 성실한 의정활동을 수행함으로<br/>다른 의원들에게 귀감이 되었습니다. 많은 격려와 칭찬 부탁드립니다.';
} else if($ratio_top <= 11) {
	$top_txt = '본 의원은 성실한 의정활동을 수행함으로<br/>국회에 긍정적인 영향을 끼쳤습니다. 격려 부탁드립니다.';
} else if($ratio_top <= 30) {
	$top_txt = '본 의원은 의정활동을 수행함에 있어<br/>뒤쳐짐 없이 성실히 노력했습니다.';
} else if($ratio_top <= 50) {
	$top_txt = '본 의원은 의정활동과 과외 활동 사이에서<br/>방황을 하는 것 같습니다. 조금 더 신경써주세요.';
} else if($ratio_top <= 70) {
	$top_txt = '의정활동에 대한 의지와 노력이 부족하며,<br/>향후 의정활동에 우려가 됩니다. 많은 관심 부탁드립니다.';
} else {
	$top_txt = '국회의원으로의 자질과 자격이 의심됩니다.<br/>매우 성실하지 못한 태도로 인해, 성실한 의정활동을 하고 있는<br/>동료 의원들에게 좋지 않은 영향을 끼치고 있습니다.';
}

try {
	$mail->Host			= "smtp.gmail.com";					// email 보낼때 사용할 서버를 지정 
	$mail->SMTPAuth		= true;								// SMTP 인증을 사용함
	$mail->Port = 465;										// email 보낼때 사용할 포트를 지정
	$mail->SMTPSecure	= "ssl";							// SSL을 사용함
	$mail->Username		= "code4yeouido@gmail.com";			// Gmail 계정
	$mail->Password		= "codeforseoul";					// 패스워드

	$mail->SetFrom('code4yeouido@gmail.com', '코드포여의도');		// 보내는 사람 email 주소와 표시될 이름 (표시될 이름은 생략가능)
	$mail->AddAddress($user_info['Email'], $user_info['Name']);			// 받을 사람 email 주소와 표시될 이름 (표시될 이름은 생략가능)
	$mail->Subject = '[코드포여의도] '.$selector_info['NameKr'].' 의원의 국회 생활통지표';						// 메일 제목
	$mail->MsgHTML("<HTML><HEAD></HEAD><BODY>
	<div style='position:relative;width:600px;height:1800px;font-family:나눔바른고딕,Nanum Barun Gothic,나눔고딕,Nanum Gothic;'>
		<img style='position:absolute;top:0;left:0;width:600px;height:1800px;z-index:2' src='http://cfy.4scour.com/images/mail/mailing_bg_cut.png'>
		
		<img style='position:absolute;top:133px;left:300px;margin-left:-55px;width:112px;' src='".$selector_info['Photo']."'>
		
		<div style='position:absolute;font-size:17.89px;color:#fff;z-index:2;text-align:center;width:600px;top:265px'>".$selector_info['NameKr']." <span style='color:#00427c'>/</span> ".$user_info['CityCompare']." ".$user_info['District']." <span style='color:#00427c'>/</span> ".$selector_info['Party']."</div>
		
		<div style='position:absolute;font-size:20.95px;color:#005096;z-index:2;text-align:center;width:600px;top:300px'>".$selector_info['NameKr']." 의원의 국회 생활통지표가 도착했습니다</div>

		<div style='position:absolute;font-size:70px;color:#7c7c7c;z-index:2;text-align:right;width:280px;left:0;top:425px'>".$ratio['AttendanceRate']."<span style='font-size:25px;'>%</span></div>
		<div style='position:absolute;font-size:70px;color:#7c7c7c;z-index:2;text-align:left;width:280px;right:0;top:425px'>".$bill_count[0]."<span style='font-size:25px;'>개</span></div>

		<div style='position:absolute;z-index:2;text-align:center;width:600px;top:708px;color:#000'>
			<table cellpadding='0' cellspacing='0' style='width:590px;margin:0 auto;'>
				".$tr1."
			</table>
		</div>

		<div style='position:absolute;z-index:2;text-align:center;width:600px;top:1098px;color:#000'>
			<table cellpadding='0' cellspacing='0' style='width:590px;margin:0 auto;'>
				".$tr2."
			</table>
		</div>

		<div style='position:absolute;font-size:15px;color:#39352a;z-index:2;text-align:center;width:600px;top:1420px;'>출석률 상위</div>

		<div style='position:absolute;font-size:20.95px;color:#39352a;z-index:2;text-align:center;width:600px;top:1455px;'>".$ratio_top."%</div>

		<div style='position:absolute;font-size:20.77px;color:#fff;z-index:2;text-align:center;width:600px;top:1505px;'>".$top_txt."</div>

		<div style='position:absolute;font-size:13.65px;color:#2d2d2d;z-index:2;text-align:center;width:600px;top:1595px;'>".$selector_info['NameKr']." 의원 / TEL : ".$selector_info['OffPhone']." / ".$selector_info['Email']."</div>
		
		<img style='position:absolute;top:1650px;left:189px;width:221px;height:52px;z-index:2;cursor:pointer' src='http://cfy.4scour.com/images/mail/spam_btn.png'>
	</div>
</BODY></HTML>");						// 메일 내용 (HTML 형식도 되고 그냥 일반 텍스트도 사용 가능함)

	$mail->Send();											// 실제로 메일을 보냄
	echo "Message Sent OK<p></p>\n";
} catch (phpmailerException $e) {
	echo $e->errorMessage();								//Pretty error messages from PHPMailer
} catch (Exception $e) {
	echo $e->getMessage();									//Boring error messages from anything else!
}*/
            }
        }
    }
}