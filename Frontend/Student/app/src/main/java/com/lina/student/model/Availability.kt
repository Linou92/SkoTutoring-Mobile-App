package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Availability {
    constructor()
    constructor(StartTime: String, EndTime: String, Date: String, Id: Int, isColoded: Boolean) {
        this.StartTime = StartTime
        this.EndTime = EndTime
        this.Date = Date
        this.Id = Id
        this.isColoded = isColoded
    }


    var StartTime:String=""
    var EndTime:String=""
    var Date:String=""
    var Id:Int=-1;
    var isColoded=false

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Availability> {
            val listType = object : TypeToken<List<Availability>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Availability>
        }

        fun fromJson(jsonObj: String): Availability {
            return Gson().fromJson(jsonObj, Availability::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}