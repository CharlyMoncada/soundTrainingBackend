CREATE TABLE tbl_history (
  id INT NOT NULL AUTO_INCREMENT,
  frequency INT NOT NULL,
  user_id INT NOT NULL,
  app_id INT NOT NULL,
  date_saved datetime default now(),
  PRIMARY KEY (id)
) ENGINE = InnoDB CHARSET=utf8;

CREATE TABLE tbl_user (
  id INT NOT NULL AUTO_INCREMENT,
  user VARCHAR(128) NOT NULL,
  first_name VARCHAR(128) NOT NULL,
  last_name VARCHAR(128) NOT NULL,
  email VARCHAR(128) NOT NULL,
  age INT NOT NULL,
  date_created DATETIME NOT NULL,
  PRIMARY KEY (id)
) ENGINE = InnoDB CHARSET=utf8;

CREATE TABLE tbl_app_type (
  id INT NOT NULL AUTO_INCREMENT,
  app_type VARCHAR(128) NOT NULL,
  PRIMARY KEY (id)
) ENGINE = InnoDB CHARSET=utf8;

Insert into tbl_app_type (app_type) VALUES ('Mobile Android');
Insert into tbl_app_type (app_type) VALUES ('Mobile IOS');
Insert into tbl_app_type (app_type) VALUES ('Web');
Insert into tbl_app_type (app_type) VALUES ('Desktop');