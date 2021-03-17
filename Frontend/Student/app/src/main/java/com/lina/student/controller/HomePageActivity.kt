package com.lina.student.controller

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Toast
import com.afollestad.materialdialogs.MaterialDialog
import com.afollestad.materialdialogs.list.listItemsSingleChoice
import com.androidnetworking.error.ANError
import com.lina.student.R
import com.lina.student.model.Teacher
import com.lina.student.network.Connection
import com.lina.student.network.ConnectionAPI
import com.lina.student.network.ConnectionURL
import com.lina.student.util.SharedPrefacesManager
import com.lina.teacher.model.Availability
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_home_page.*
import kotlinx.android.synthetic.main.activity_register.*
import org.json.JSONObject

class HomePageActivity : AppCompatActivity() {


    lateinit var levelNameList: ArrayList<String>
    var levelId:Int=1
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home_page)

        levelNameList = ArrayList<String>()
        levelNameList.add("level 10 ")
        levelNameList.add("level 11 ")
        levelNameList.add("level 12 ")


        et_level.setOnClickListener {
            MaterialDialog(this).show {
                listItemsSingleChoice(items = levelNameList) { dialog, indices, items ->
                    levelId =indices+1
                    var name: String = levelNameList[indices]
                    this@HomePageActivity.et_level.setText(name)
                }
            }

        }

        btn_search.setOnClickListener {
            filter()
        }
    }

    private fun filter(){

        var lan=et_language.text.toString()
        var sub=et_subject.text.toString()

        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.filter+"?language="+lan+"&levelId="+levelId+"&subject="+sub,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.filter,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                        var  teachers = Teacher.fromJson(
                            objectJs.optJSONArray("Items")
                        )

                        var aviId=ArrayList<String>()
                        var languageStringList= java.util.ArrayList<String>()

                        for(item in teachers) {

                            for(item1 in item.Availabilities)
                            {

                                aviId.add(item1.Id.toString())
                                var startMinit=(item1.StartTime).toInt()%60
                                var startHours=(item1.StartTime).toInt()/60

                                var endMinit=(item1.EndTime).toInt()%60
                                var endHours=(item1.EndTime).toInt()/60


                                var avi:String=item.FullName+"\n"+item1.Date+"  from "+startHours+":"+startMinit+"  to "+endHours+":"+endMinit
                                languageStringList.add(avi)
                            }

                        }

                        var arrayAdapter = ArrayAdapter( this@HomePageActivity,
                            android.R.layout.simple_list_item_1, languageStringList)
                        this@HomePageActivity.lv_res.adapter = arrayAdapter

                        this@HomePageActivity.lv_res.setOnItemClickListener {parent, view, position, id ->
                            requestSession(aviId[position])
                        }


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



    private fun requestSession( id:String ){
        var lan=et_language.text.toString()
        var sub=et_subject.text.toString()
        val jsonObject = JSONObject()
        jsonObject.put("AvailabilityId",id)
        jsonObject.put("Subject",sub)
        jsonObject.put("Level",levelId)
        jsonObject.put("Language",lan)
        jsonObject.put("Title","course")
        Connection.post(


            ConnectionURL.MAIN_URL + ConnectionAPI.req, jsonObject,SharedPrefacesManager.authToken,
            ConnectionAPI.req, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {
                    Log.i("AndroidNetworking", "onSubscribe")
                    Toast.makeText(this@HomePageActivity,"Done",Toast.LENGTH_LONG).show()
                }

                override fun onSubscribe(d: Disposable) {
                    Log.i("AndroidNetworking", "onSubscribe")
                    Toast.makeText(this@HomePageActivity,"Done",Toast.LENGTH_LONG).show()

                }

                override fun onNext(objectJs: JSONObject) {
                    Log.i("AndroidNetworking", "onSubscribe")
                }

                override fun onError(e: Throwable) {
                    Log.i("AndroidNetworking", "onSubscribe")
                }
            })
    }
}