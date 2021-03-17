package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Session {
    constructor()


    var Date:String=""
    var Student:String=""

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Session> {
            val listType = object : TypeToken<List<Session>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Session>
        }

        fun fromJson(jsonObj: String): Session {
            return Gson().fromJson(jsonObj, Session::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}