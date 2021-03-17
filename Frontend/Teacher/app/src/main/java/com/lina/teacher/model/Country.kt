package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Country {
    constructor()
    constructor(Name: String, Id: Int) {
        this.Name = Name
        this.Id = Id
    }


    var Name:String=""
    var Id:Int=-1;

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Country> {
            val listType = object : TypeToken<List<Country>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Country>
        }

        fun fromJson(jsonObj: String): Country {
            return Gson().fromJson(jsonObj, Country::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}