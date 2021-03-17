package com.lina.teacher.controller

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.model.Profile
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_user_info.*
import org.json.JSONObject

class UserInfoActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_user_info)

        getUserInfo()
    }
    private fun getUserInfo(){

        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetUserInfo,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetUserInfo,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONObject("Item") != null) {


                        var jsonString :String = objectJs.optJSONObject("Item").toString()
                        var  profile: Profile = Profile.fromJson(
                            jsonString
                        )


                        this@UserInfoActivity.tv_first_name.setText(profile.FirstName)
                        this@UserInfoActivity.tv_last_name.setText(profile.LastName)
                        this@UserInfoActivity.tv_user_name.setText(profile.UserName)
                        this@UserInfoActivity.tv_email.setText(profile.EmailAddress)
                        this@UserInfoActivity.tv_role.setText(profile.Role)



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