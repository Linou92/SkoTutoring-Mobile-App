package com.lina.teacher.repository

import android.util.Log
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.TemplateApplication
import com.lina.teacher.model.Country
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import io.reactivex.disposables.Disposable
import io.reactivex.subjects.PublishSubject
import org.json.JSONObject

class HomeRepository {
    public val errorMessageObserver = PublishSubject.create<String>()
    public val successObserver = PublishSubject.create<Boolean>()
    public val tokenSuccessObserver = PublishSubject.create<String>()


    public fun signup(firstName: String,lastNAme: String,UserName: String,password: String,countryId: String) {
        val jsonObj = JSONObject()
        jsonObj.put("FirstName", firstName)
        jsonObj.put("LastName", lastNAme)
        jsonObj.put("UserName", UserName)
        jsonObj.put("Password", password)
        jsonObj.put("CountryId", countryId)


       Connection.post(ConnectionURL.MAIN_URL + ConnectionAPI.Register, jsonObj,ConnectionAPI.Register, object : io.reactivex.Observer<JSONObject> {
            override fun onComplete() {

            }

            override fun onSubscribe(d: Disposable) {
                Log.i("AndroidNetworking", "onSubscribe")
            }

            override fun onNext(objectJs: JSONObject) {
                if (objectJs.optJSONObject("Message") != null && objectJs.optJSONObject("Message").optString("Content") != null) {
                    tokenSuccessObserver.onNext(objectJs.optJSONObject("Message").optString("Content"))
                } else if (objectJs.opt("message") != null) {
                }

            }

            override fun onError(e: Throwable) {
                if (e is ANError) {
                    try {
                        val objectJs = JSONObject(e.errorBody)
                        if (objectJs.opt("message") != null) {
                            errorMessageObserver.onNext(objectJs.optString("message"))
                        } else {
                        }

                    } catch (e: Exception) {
                    }

                }
            }

        })
    }

    public fun login(username: String, password: String) {
        val jsonObj = JSONObject()
        jsonObj.put("username", username)
        jsonObj.put("password", password)
        jsonObj.put("grant_type", "password")

        Connection.post(
            ConnectionURL.MAIN_URL + ConnectionAPI.LogIn,
            jsonObj,
            ConnectionAPI.LogIn,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {
                    Log.i("AndroidNetworking", "onSubscribe")
                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optString("access_token") != null) {
                        tokenSuccessObserver.onNext(objectJs.optString("access_token"))
                    } else if (objectJs.opt("error_description") != null) {
                        errorMessageObserver.onNext(objectJs.optString("error_description"))
                    }
                }

                override fun onError(e: Throwable) {
                    if (e is ANError) {
                        try {
                            val objectJs = JSONObject(e.errorBody)
                            if (objectJs.opt("error_description") != null) {
                                errorMessageObserver.onNext(objectJs.optString("error_description"))
                            } else {
                            }

                        } catch (e: Exception) {
                        }

                    }
                }

            })
    }


    public fun getHomePageReports(): PublishSubject<ArrayList<Country>> {
        val homePageReportsObserver = PublishSubject.create<ArrayList<Country>>()
        val jsonObject = JSONObject()

        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetAllCountries,
            jsonObject, "",
            ConnectionAPI.GetAllCountries,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {
                        homePageReportsObserver.onNext(
                            Country.fromJson(
                                objectJs.optJSONArray("Items")
                            )
                        )
                    } else if (objectJs.opt("Message") != null) {
                        errorMessageObserver.onNext(objectJs.optString("message"))
                    }

                }

                override fun onError(e: Throwable) {
                    if (e is ANError) {
                        try {
                            val objectJs = JSONObject(e.errorBody)
                            if (objectJs.opt("Message") != null) {
                                errorMessageObserver.onNext(objectJs.optString("Message"))
                            } else {
                                errorMessageObserver.onNext(
                                    TemplateApplication.getApplicationInstance()
                                        .getString(R.string.connection_error_message)
                                )
                            }

                        } catch (e: Exception) {
                            errorMessageObserver.onNext(
                                TemplateApplication.getApplicationInstance()
                                    .getString(R.string.connection_error_message)
                            )
                        }

                    }
                }

            })
        return homePageReportsObserver
    }

}