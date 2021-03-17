package com.lina.teacher.controller

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ArrayAdapter
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.model.Availability
import com.lina.teacher.model.Session
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_availability.*
import kotlinx.android.synthetic.main.activity_sessions.*
import org.json.JSONObject
import java.util.ArrayList

class SessionsActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sessions)

        getAvil()
    }

    private fun getAvil(){
        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetMyFeat,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetMyFeat,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                        var  Sessions = Session.fromJson(
                            objectJs.optJSONArray("Items")
                        )

                        var languageStringList= ArrayList<String>()
                        for(item in Sessions) {

                            var avi:String=item.Student+"       "+item.Date
                            languageStringList.add(avi)
                        }
                        var arrayAdapter = ArrayAdapter( this@SessionsActivity,
                            android.R.layout.simple_list_item_1, languageStringList)
                        this@SessionsActivity.lv_session.adapter = arrayAdapter


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