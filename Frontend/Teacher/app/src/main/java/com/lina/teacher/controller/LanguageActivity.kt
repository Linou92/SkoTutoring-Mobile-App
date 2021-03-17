package com.lina.teacher.controller

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Toast
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.model.Language
import com.lina.teacher.model.Profile
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_language.*
import kotlinx.android.synthetic.main.activity_user_info.*
import org.json.JSONObject

class LanguageActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_language)



        getLangs()

        btn_add_lang.setOnClickListener {

            addlang()

        }
    }


    private fun addlang(){
        var lang:String=et_addLang.text.toString()
        if(lang==null  || lang==null)
        {
            Toast.makeText(this,"Please type the language",Toast.LENGTH_LONG).show()
            return
        }
        val jsonObject = JSONObject()
        jsonObject.put("Name",lang)
        Connection.post(


            ConnectionURL.MAIN_URL + ConnectionAPI.addLang, jsonObject,SharedPrefacesManager.authToken,
            ConnectionAPI.addLang, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {
                    getLangs()
                }

                override fun onSubscribe(d: Disposable) {
                    Log.i("AndroidNetworking", "onSubscribe")
                }

                override fun onNext(objectJs: JSONObject) {
                }

                override fun onError(e: Throwable) {

                }
            })

    }
    private fun getLangs(){
        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetLangs,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetLangs,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                        var  languages = Language.fromJson(
                            objectJs.optJSONArray("Items")
                        )

                        var languageStringList=ArrayList<String>()
                        for(item in languages)
                            languageStringList.add(item.Name)

                       var arrayAdapter = ArrayAdapter( this@LanguageActivity,
                            android.R.layout.simple_list_item_1, languageStringList)
                        this@LanguageActivity.lv_langs.adapter = arrayAdapter


                    } else if (objectJs.opt("Message") != null) {
                        // errorMessageObserver.onNext(objectJs.optString("message"))
                    }

                }

                override fun onError(e: Throwable) {
                    if (e is ANError) {
                        try {
                            val objectJs = JSONObject(e.errorBody)
                            if (objectJs.opt("Message") != null) {
                            } else {
                            }
                        } catch (e: Exception) {
                        }
                    }
                }

            })



    }
}