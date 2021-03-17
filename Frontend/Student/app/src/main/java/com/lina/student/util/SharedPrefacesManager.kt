package com.lina.student.util


import androidx.preference.PreferenceManager
import com.lina.student.TemplateApplication

class SharedPrefacesManager {
    companion object {
        private const val AUTH_TOKEN = "token"

        var authToken: String
            get() {
                val prefs = PreferenceManager.getDefaultSharedPreferences(TemplateApplication.getApplicationInstance())
                return prefs.getString(AUTH_TOKEN, "")!!
            }
            set(str) {
                val editor = PreferenceManager.getDefaultSharedPreferences(TemplateApplication.getApplicationInstance()).edit()
                editor.putString(AUTH_TOKEN, str)
                editor.apply()
            }
    }
}