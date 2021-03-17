package com.lina.teacher.helper

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import org.json.JSONArray
import java.util.ArrayList

class JsonHelper {
    companion object {
        fun objectToString(o: Any): String {
            val gson = Gson()
            try {
                var temp = gson.toJsonTree(o).toString()
                return temp
            } catch (e: Exception) {
                e.printStackTrace()
                return ""
            }
        }
        fun fromJsonArrayString(jsonObj: JSONArray): ArrayList<String> {
            val listType = object : TypeToken<List<String>>() {}.type
            return Gson().fromJson(jsonObj.toString(), listType) as ArrayList<String>
        }
    }
}