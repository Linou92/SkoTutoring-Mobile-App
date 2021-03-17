package com.lina.teacher.controller

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Toast
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.model.Language
import com.lina.teacher.model.Subject
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_language.*
import kotlinx.android.synthetic.main.activity_subjects.*
import org.json.JSONObject

class SubjectsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_subjects)

        getSub()
        btn_add_sub.setOnClickListener {
            addSub()
        }
    }

    private fun addSub(){
        var subject:String=et_addsub.text.toString()
        if(subject==null  || subject==null)
        {
            Toast.makeText(this,"Please type the subject", Toast.LENGTH_LONG).show()
            return
        }
        val jsonObject = JSONObject()
        jsonObject.put("Name",subject)
        Connection.post(


            ConnectionURL.MAIN_URL + ConnectionAPI.addSubject, jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.addSubject, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {
                    getSub()
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
    private fun getSub(){
        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetMySubjects,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetMySubjects,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                        var  subjects = Subject.fromJson(
                            objectJs.optJSONArray("Items")
                        )

                        var languageStringList=ArrayList<String>()
                        for(item in subjects)
                            languageStringList.add(item.Name)

                        var arrayAdapter = ArrayAdapter( this@SubjectsActivity,
                            android.R.layout.simple_list_item_1, languageStringList)
                        this@SubjectsActivity.lv_subjects.adapter = arrayAdapter


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