<?php
include("bl_Common.php");

$name = $_POST['name'];
$pass = $_POST['password'];

$link=dbConnect();
 
$name = stripslashes($name);
$pass = stripslashes($pass);
$name = mysqli_real_escape_string($link,$name);
$pass = mysqli_real_escape_string($link,$pass);

if(empty($name) ){
die("You don't have permission for this");
}

$check = mysqli_query($link,"SELECT * FROM MyGameDB WHERE `name` ='$name' ") or die(mysqli_error($link));
$numrows = mysqli_num_rows($check);

if ($numrows == 0)
{
	die ("001");//user not exist
}
else
{
	$pass = md5($pass);
	while($row = mysqli_fetch_assoc($check))
	{
		if ($pass == $row['password']){
		if($row['active'] == "1"){ 
                        echo "success";
                        echo "|";
                        echo $row['name'];
                        echo "|";
						echo $row['nick'];
                        echo "|";
                        echo $row['kills'];
                        echo "|";
                        echo $row['deaths'];
                        echo "|";
                        echo $row['score'];
                        echo "|";
						echo $row['playtime'];
                        echo "|";
			            echo $row['status'];
                        echo "|";
                        echo $row['id'];
                        echo "|";
						echo $row['flist'];
                        echo "|";
						echo $row['coins'];
                        echo "|";
                        echo $row['uIP'];
                        echo "|";
                        echo $row['clan'] . "|";
                        echo $row['clan_invitations'] . "|";
						  }else{
						  die("007");
						  }
       
                } else {
			die("002");//wrong password
	}
                }
}
mysqli_close($link);
?>