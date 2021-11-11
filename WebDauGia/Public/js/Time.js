function renderTime() {
    var mydate = new Date();
    var year = mydate.getYear();

    if (year < 1000) {
        year += 1900;
    }

    var day = mydate.getDay();
    var month = mydate.getMonth();
    var daym = mydate.getDate();
    var dayarray = new Array("Chủ nhật","Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy");
    var montharray = new Array("01", '02', "03","04","05","06","07","08","09","10","11","12");

    //Time
    var currentTime = new Date();
    var hours = currentTime.getHours();
    var minutes = currentTime.getMinutes();
    var seconds = currentTime.getSeconds();

    if (hours == 24) {
        hours = 0;
    } else if (hours > 12) {
        hours = hours - 0;
    }

    if (hours < 10) {
        hours = "0" + hours;
    }
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    if (seconds < 10) {
        seconds = "0" + seconds;
    }

    var myClock = document.getElementById("clockDisplay");
    myClock.innerText = "" + hours + ":" + minutes + ":" + seconds + " " + dayarray[day] + ", " + daym + "/" + montharray[month] + "/" + year + "";

    setTimeout("renderTime()", 1000);
}
renderTime();
