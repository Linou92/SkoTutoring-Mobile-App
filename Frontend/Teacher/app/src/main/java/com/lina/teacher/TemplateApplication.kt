package com.lina.teacher

import androidx.multidex.MultiDexApplication
import com.androidnetworking.AndroidNetworking

class TemplateApplication : MultiDexApplication(){

    companion object {
        private lateinit var applicationInstance:TemplateApplication
        fun getApplicationInstance():TemplateApplication{
            return applicationInstance
        }
    }

    override fun onCreate() {
        super.onCreate()
         AndroidNetworking.initialize(applicationContext)
       // FirebaseApp.initializeApp(this)
        applicationInstance = this
    }

}