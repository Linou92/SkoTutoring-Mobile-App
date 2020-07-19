var db;
var dbCreated = false;

var scroll = new iScroll('wrapper', {
 vScrollbar : false,
 hScrollbar : false,
 hScroll : false
});
document.addEventListener("deviceready", onDeviceReady, false);

function onDeviceReady() {

 var country = document.getElementById("country").value;
 var username = document.getElementById("username").value;
 var password = document.getElementById("password").value;
 var passwordConf = document.getElementById("passwordConf").value;

 db = window.openDatabase("RegistrationDB", "1.0", "Registration", 200000);
 if (dbCreated)
 else
  db.transaction(populateDB, transaction_error, populateDB_success);
}

function populateDB(tx) {
 tx.executeSql('DROP TABLE IF EXISTS Registration');
 var sql = "CREATE TABLE IF NOT EXISTS Registration ( "
   + "username VARCHAR(50), " + "country VARCHAR(50), "
   + "password VARCHAR(200), "
   + "passwordConf VARCHAR(200))";
 tx.executeSql(sql);
 var username = document.getElementById("username").value;
 var country =  document.getElementById("country").value;
 var password =document.getElementById("password").value;
 var passwordConf = document.getElementById("passwordConf").value;
 tx.executeSql("INSERT INTO Registration (username,country,age,password,passwordConf) VALUES ('"+ username +"','"+ country +"' , "+ password+", '"+ passwordConf "' )");
 
}

function transaction_error(tx, error) {
 alert("Database Error: " + error);
}
  
function populateDB_success() {
 dbCreated = true;
 
 // where you want to move
 alert("Successfully inserted");
  window.location="../login.html";
}