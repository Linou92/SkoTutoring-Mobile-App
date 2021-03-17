package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Profile {
    constructor()
    constructor(
        FirstName: String,
        LastName: String,
        UserName: String,
        EmailAddress: String,
        Role: String
    ) {
        this.FirstName = FirstName
        this.LastName = LastName
        this.UserName = UserName
        this.EmailAddress = EmailAddress
        this.Role = Role
    }


    var FirstName:String=""
    var LastName:String=""
    var UserName:String=""
    var EmailAddress:String=""
    var Role:String=""

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Profile> {
            val listType = object : TypeToken<List<Profile>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Profile>
        }

        fun fromJson(jsonObj: String): Profile {
            return Gson().fromJson(jsonObj, Profile::class.java)
        }

    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}