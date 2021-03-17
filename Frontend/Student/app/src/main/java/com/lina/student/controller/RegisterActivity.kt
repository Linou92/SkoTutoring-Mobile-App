package com.lina.student.controller

import android.app.Activity
import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.MediaStore
import android.util.Log
import android.widget.Toast
import com.afollestad.materialdialogs.MaterialDialog
import com.afollestad.materialdialogs.list.listItemsSingleChoice
import com.androidnetworking.error.ANError
import com.lina.student.R
import com.lina.student.model.Country
import com.lina.student.network.Connection
import com.lina.student.network.ConnectionAPI
import com.lina.student.network.ConnectionURL
import com.lina.student.repository.HomeRepository
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_register.*
import org.json.JSONObject
import java.io.File

class RegisterActivity : AppCompatActivity() {

    lateinit var countiesList: ArrayList<Country>
    lateinit var countriesNameList: ArrayList<String>
    lateinit var file: File
    var countryId: Int = -2
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)


        val homeRepository: HomeRepository = HomeRepository()
        countriesNameList = ArrayList<String>()
        val res = homeRepository.getHomePageReports().subscribe {
            countiesList = ArrayList()
            countiesList.addAll(it as ArrayList<Country>)

            countriesNameList = ArrayList<String>()
            for (item in countiesList)
                countriesNameList.add(item.Name)
        }


        tv_country.setOnClickListener {
            showCountries()

        }
        btn_register.setOnClickListener {
            var firstName = tv_first_name.text.toString()
            var lasName = tv_last_name.text.toString()
            var userNAme = tv_user_name.text.toString()
            var password = tv_password.text.toString()
            var conPassword = tv_password_confirm.text.toString()

            if (firstName == null || firstName == "" || lasName == null || lasName == "" || userNAme == null
                || userNAme == "" || password == "" || password == null || conPassword == "" || conPassword == null || countryId == -2
            ) {
                Toast.makeText(this, "Please fill all data", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }
            if (password != conPassword) {
                Toast.makeText(this, "Password error", Toast.LENGTH_LONG).show()

                return@setOnClickListener
            }
            val jsonObj = JSONObject()
            jsonObj.put("FirstName", firstName)
            jsonObj.put("LastName", lasName)
            jsonObj.put("UserName", userNAme)
            jsonObj.put("Password", password)
            jsonObj.put("ConfirmPassword", conPassword)
            jsonObj.put("CountryId", countryId)
            SignUP(jsonObj)
        }

    }

    private fun SignUP(jsonObj: JSONObject) {
        Connection.post(
            ConnectionURL.MAIN_URL + ConnectionAPI.Register, jsonObj,
            ConnectionAPI.Register, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {
                    Log.i("AndroidNetworking", "onSubscribe")
                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONObject("Message") != null && objectJs.optJSONObject("Message")
                            .optString("Content") != null
                    ) {


                        Toast.makeText(
                            this@RegisterActivity,
                            objectJs.optJSONObject("Message").optString("Content"),
                            Toast.LENGTH_LONG
                        ).show()
                        if(objectJs.optJSONObject("Message").optString("Content")=="Success")
                        {
                            val intent= Intent(this@RegisterActivity,LogInActivity::class.java)
                            startActivity(intent)
                        }

                    } else if (objectJs.opt("message") != null) {
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

    private fun showCountries() {
        MaterialDialog(this).show {
            listItemsSingleChoice(items = countriesNameList) { dialog, indices, items ->
                countryId = countiesList[indices].Id
                var name: String = countiesList[indices].Name
                this@RegisterActivity.tv_country.setText(name)
            }
        }
    }


    fun getRealPathFromURI(contentURI: Uri, activity: Activity): String {
        val result: String
        val cursor = activity.contentResolver.query(contentURI, null, null, null, null)
        if (cursor == null) { // Source is Dropbox or other similar local file path
            result = contentURI.getPath()!!
        } else {
            cursor.moveToFirst()
            val idx = cursor.getColumnIndex(MediaStore.Images.ImageColumns.DATA)
            result = cursor.getString(idx)
            cursor.close()
        }
        return result
    }


}