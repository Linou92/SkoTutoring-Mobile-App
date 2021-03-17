package com.lina.student.controller

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import com.androidnetworking.error.ANError
import com.lina.student.R
import com.lina.student.network.Connection
import com.lina.student.network.ConnectionAPI
import com.lina.student.network.ConnectionURL
import com.lina.student.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_log_in.*
import org.json.JSONObject

class LogInActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_log_in)


        btn_log_in.setOnClickListener {
            var username:String=tv_username.text.toString()
            var pass:String=tv_pass.text.toString()

            LogIn(username,pass)

        }

        tv_register.setOnClickListener {
            val intent= Intent(this,RegisterActivity::class.java)
            startActivity(intent)
        }

    }

    private fun LogIn(username:String,pass:String){
        val jsonObj = JSONObject()
        jsonObj.put("username",username)
        jsonObj.put("password",pass)
        Connection.post(
            ConnectionURL.MAIN_URL + ConnectionAPI.LogIn, jsonObj,
            ConnectionAPI.LogIn, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {
                    Log.i("AndroidNetworking", "onSubscribe")
                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optString("Token") != null && objectJs.optString("Token") != "null") {
                        SharedPrefacesManager.authToken=objectJs.optString("Token")
                        val intent= Intent(this@LogInActivity,HomePageActivity::class.java)
                        startActivity(intent)


                    } else  {
                        Toast.makeText(this@LogInActivity,"error", Toast.LENGTH_LONG).show()
                    }

                }

                override fun onError(e: Throwable) {
                    if (e is ANError) {
                        try {
                            val objectJs = JSONObject(e.errorBody)
                            if (objectJs.opt("message") != null) {
                            } else {
                            }
                        } catch (e: Exception) {
                        }
                    }
                }
            })


    }
}