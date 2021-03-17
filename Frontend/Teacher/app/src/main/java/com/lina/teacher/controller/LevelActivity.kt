package com.lina.teacher.controller

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Toast
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.model.Language
import com.lina.teacher.model.Level
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_language.*
import kotlinx.android.synthetic.main.activity_level.*
import org.json.JSONObject

class LevelActivity : AppCompatActivity() {

    lateinit var levels:ArrayList<Level>
    lateinit var myLevels:ArrayList<Level>
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_level)

        getAllLevel()
        getMyLevel()


        btn_add_level10.setOnClickListener {
            addLevel("1")
        }
        btn_add_level11.setOnClickListener {
            addLevel("2")
        }
        btn_add_level12.setOnClickListener {
            addLevel("3")
        }
    }

    private fun addLevel(id:String){

        for(item in myLevels)
            if(item.Id.toString()==id) return


        val jsonObject = JSONObject()

        Connection.post(
            ConnectionURL.MAIN_URL + ConnectionAPI.addLevel+"?LevelId="+id, jsonObject,SharedPrefacesManager.authToken,
            ConnectionAPI.addLevel, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {
                    getMyLevel()
                }

                override fun onSubscribe(d: Disposable) {
                    getMyLevel()
                }

                override fun onNext(objectJs: JSONObject) {
                    getMyLevel()
                }

                override fun onError(e: Throwable) {

                }
            })

    }


    private fun getAllLevel(){

        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetAllLevel,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetMyLevel,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                          levels = Level.fromJson(
                            objectJs.optJSONArray("Items")
                        )




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


    private fun getMyLevel(){

        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetMyLevel,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetMyLevel,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                        myLevels = Level.fromJson(
                            objectJs.optJSONArray("Items")
                        )

                        var levelStringList=ArrayList<String>()
                        for(item in myLevels)
                            levelStringList.add(item.Name)

                        var arrayAdapter = ArrayAdapter( this@LevelActivity,
                            android.R.layout.simple_list_item_1, levelStringList)
                        this@LevelActivity.lv_levels.adapter = arrayAdapter



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