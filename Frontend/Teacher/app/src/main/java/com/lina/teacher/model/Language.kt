package com.lina.teacher.model

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class Language {
    constructor()
    constructor(Name: String, Id: Int) {
        this.Name = Name
        this.Id = Id
    }


    var Name:String=""
    var Id:Int=-1;

    companion object {
        fun fromJson(jsonObj: JSONArray): ArrayList<Language> {
            val listType = object : TypeToken<List<Language>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<Language>
        }

        fun fromJson(jsonObj: String): Language {
            return Gson().fromJson(jsonObj, Language::class.java)
        }
    }
    public fun toJson(): String {
        return Gson().toJson(this)
    }
}