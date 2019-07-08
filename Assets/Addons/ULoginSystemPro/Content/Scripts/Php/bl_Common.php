<?php
$GameVersion = "1.5";
$hostName    = 'HOST_NAME_HERE';
$dbName      = 'DATABASE_NAME_HERE';
$dbUser      = 'DATABASE_USERNAME_HERE';
$dbPassworld = 'DATABASE_PASSWORLD_HERE';
$secretKey   = "123456";
$base_url    = 'http://domain.com/game/php/';
$emailFrom   = 'example@gmail.com';
$GameName    = "Game Name Here";

function dbConnect()
{
    global $dbName;
    global $secretKey;
    global $hostName;
    global $dbUser;
    global $dbPassworld;
    
    $link = mysqli_connect($hostName, $dbUser, $dbPassworld, $dbName);
    
    if (!$link) {
        fail("Couldn´t connect to database server");
    }
    
    return $link;
}

function TrydbConnect()
{
    global $dbName;
    global $secretKey;
    global $hostName;
    global $dbUser;
    global $dbPassworld;
    
    $link = @mysqli_connect($hostName, $dbUser, $dbPassworld, $dbName) or die("2");
    return $link;
}

function safe($variable)
{
    $variable = addslashes(trim($variable));
    return $variable;
}

function fail($errorMsg)
{
    print $errorMsg;
    exit;
}

?>