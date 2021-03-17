package com.lina.teacher.network

class ConnectionAPI {
    companion object {
        const val Register = "Api/Account/Register"
        const val GetAllCountries = "Api/Account/GetAllCountries"
        const val LogIn = "Api/Account/RequestToken"
        const val GetUserInfo = "/Api/Account/GetUserInfo"

        const val GetLangs = "/Api/Languages/List"
        const val addLang = "Api/Languages/RegisterLanguage"

        const val GetMyLevel = "Api/Levels/MyList"
        const val GetAllLevel = "Api/Levels/List"
        const val addLevel = "Api/Levels/RegisterMyLevel"

        const val GetMySubjects = "Api/Subjects/List"
        const val addSubject = "Api/Subjects/RegisterSubject"


        const val GetMyAvi = "Api/Availability/List"
        const val addAvi = "Api/Availability/RegisterAvailability"

        const val GetMyFeat = "Api/Sessions/GetMyPendingSessions"


    }
}