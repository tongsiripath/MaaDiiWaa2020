CREATE TABLE `MyGameDB` (
  `id` int(10) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` varchar(30) CHARACTER SET latin1 COLLATE latin1_general_cs NOT NULL,
  `nick` varchar(20) CHARACTER SET latin1 COLLATE latin1_general_cs NOT NULL,
  `password` varchar(50) NOT NULL,
  `kills` int(11) NOT NULL DEFAULT '0',
  `deaths` int(11) NOT NULL DEFAULT '0',
  `score` int(11) NOT NULL DEFAULT '0',
  `coins` int(11) NOT NULL DEFAULT '0',
  `purchases` varchar(200) DEFAULT NULL,
  `clan` varchar(12) NOT NULL DEFAULT '-1',
  `clan_invitations` varchar(50) NOT NULL DEFAULT '1,',
  `playtime` int(50) NOT NULL DEFAULT '0',
  `email` varchar(30) NOT NULL,
  `active` int(1) NOT NULL DEFAULT '0',
  `uIP` varchar(50) NOT NULL DEFAULT 'none',
  `flist` varchar(200) DEFAULT NULL,
  `status` int(3) NOT NULL DEFAULT '0',
  `verify` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `MyGameTickets` (
  `id` int(10) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` varchar(30) CHARACTER SET latin1 COLLATE latin1_general_cs NOT NULL,
  `title` varchar(25) NOT NULL,
  `content` varchar(300) NOT NULL,
  `reply` varchar(300) NOT NULL DEFAULT '',
  `close` int(9) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `BanList` (
  `id` int(10) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `name` varchar(30) CHARACTER SET latin1 COLLATE latin1_general_cs NOT NULL,
  `reason` varchar(125) NOT NULL,
  `myIP` varchar(50) NOT NULL,
  `mBy` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;