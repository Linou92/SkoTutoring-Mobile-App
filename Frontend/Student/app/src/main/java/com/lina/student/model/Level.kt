package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Level {
    constructor()
    constructor(Name: String, Id: Int) {
        this.Name = Name
        this.Id = Id
    }


    var Name:String=""
    var Id:Int=-1;

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Level> {
            val listType = object : TypeToken<List<Level>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Level>
        }

        fun fromJson(jsonObj: String): Level {
            return Gson().fromJson(jsonObj, Level::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}