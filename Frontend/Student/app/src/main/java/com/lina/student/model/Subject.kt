package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Subject {
    constructor()
    constructor(Name: String, Id: Int) {
        this.Name = Name
        this.Id = Id
    }


    var Name:String=""
    var Id:Int=-1;

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Subject> {
            val listType = object : TypeToken<List<Subject>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Subject>
        }

        fun fromJson(jsonObj: String): Subject {
            return Gson().fromJson(jsonObj, Subject::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}