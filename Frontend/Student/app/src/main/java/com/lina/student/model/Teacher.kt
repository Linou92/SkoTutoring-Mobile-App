package com.lina.student.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import com.lina.teacher.model.Availability
import com.lina.teacher.model.Level
import org.json.JSONArray
import java.util.ArrayList

class Teacher {

    constructor()


    var FullName:String=""
    var Id:Int=-1;

    var Availabilities:ArrayList<Availability> = ArrayList()

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Teacher> {
            val listType = object : TypeToken<List<Teacher>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Teacher>
        }

        fun fromJson(jsonObj: String): Teacher {
            return Gson().fromJson(jsonObj, Teacher::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}