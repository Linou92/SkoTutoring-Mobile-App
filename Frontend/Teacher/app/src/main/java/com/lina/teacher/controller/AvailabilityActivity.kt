package com.lina.teacher.controller

import android.app.DatePickerDialog
import android.app.TimePickerDialog
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.DatePicker
import android.widget.Toast
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.model.Availability
import com.lina.teacher.model.Language
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_availability.*
import kotlinx.android.synthetic.main.activity_language.*
import org.json.JSONObject
import java.text.SimpleDateFormat
import java.util.*

class AvailabilityActivity : AppCompatActivity(), DatePickerDialog.OnDateSetListener {

    lateinit var myCalendar: Calendar


     var date:String=""
     var startTime:Int=-1
     var endTime:Int=-1


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_availability)



        myCalendar = Calendar.getInstance()
        et_date.setOnClickListener {
            val startTime = DatePickerDialog(
                this@AvailabilityActivity, this, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                myCalendar.get(Calendar.DAY_OF_MONTH)
            )
            startTime.show()
        }

        et_time_start.setOnClickListener {

            val currentTime = Calendar.getInstance()
            val hour = currentTime.get(Calendar.HOUR_OF_DAY)
            val minute = currentTime.get(Calendar.MINUTE)
            val mTimePicker: TimePickerDialog
            mTimePicker = TimePickerDialog(this@AvailabilityActivity, TimePickerDialog.OnTimeSetListener { timePicker, selectedHour, selectedMinute ->
                et_time_start.setText("$selectedHour:$selectedMinute")

                startTime=selectedHour*60+selectedMinute
            }, hour, minute, true)//Yes 24 hour time
            mTimePicker.setTitle("Select Time")
            mTimePicker.setMessage("Please select the time that suits you")

            mTimePicker.show()

        }

        et_time_end.setOnClickListener {

            val currentTime = Calendar.getInstance()
            val hour = currentTime.get(Calendar.HOUR_OF_DAY)
            val minute = currentTime.get(Calendar.MINUTE)
            val mTimePicker: TimePickerDialog
            mTimePicker = TimePickerDialog(this@AvailabilityActivity, TimePickerDialog.OnTimeSetListener { timePicker, selectedHour, selectedMinute ->
                et_time_end.setText("$selectedHour:$selectedMinute")

                endTime=selectedHour*60+selectedMinute
            }, hour, minute, true)//Yes 24 hour time
            mTimePicker.setTitle("Select Time")
            mTimePicker.setMessage("Please select the time that suits you")

            mTimePicker.show()

        }



        getAvil()

        btn_add_avi.setOnClickListener {
            addAvil()
        }

    }


    private fun addAvil(){

        if(startTime==-1 || endTime==-1 || date=="")
        {
            Toast.makeText(this,"Please select date, start and end time", Toast.LENGTH_LONG).show()
            return
        }
        val jsonObject = JSONObject()
        jsonObject.put("StartTime",startTime)
        jsonObject.put("EndTime",endTime)
        jsonObject.put("Date",date)
        Connection.post(


            ConnectionURL.MAIN_URL + ConnectionAPI.addAvi, jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.addAvi, object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {
                    getAvil()
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
    private fun getAvil(){
        val jsonObject = JSONObject()
        Connection.get(
            ConnectionURL.MAIN_URL + ConnectionAPI.GetMyAvi,
            jsonObject, SharedPrefacesManager.authToken,
            ConnectionAPI.GetMyAvi,
            object : io.reactivex.Observer<JSONObject> {
                override fun onComplete() {

                }

                override fun onSubscribe(d: Disposable) {

                }

                override fun onNext(objectJs: JSONObject) {
                    if (objectJs.optJSONArray("Items") != null) {



                        var  availabilities = Availability.fromJson(
                            objectJs.optJSONArray("Items")
                        )

                        var languageStringList=ArrayList<String>()
                        for(item in availabilities) {
                            var startMinit=(item.StartTime).toInt()%60
                            var startHours=(item.StartTime).toInt()/60

                            var endMinit=(item.EndTime).toInt()%60
                            var endHours=(item.EndTime).toInt()/60


                         var avi:String=item.Date+"  from "+startHours+":"+startMinit+"  to "+endHours+":"+endMinit
                            languageStringList.add(avi)
                        }
                        var arrayAdapter = ArrayAdapter( this@AvailabilityActivity,
                            android.R.layout.simple_list_item_1, languageStringList)
                        this@AvailabilityActivity.lv_avi.adapter = arrayAdapter


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


    override fun onDateSet(p0: DatePicker?, year: Int, month: Int, dayOfMonth: Int) {
        myCalendar.set(Calendar.YEAR, year)
        myCalendar.set(Calendar.MONTH, month)
        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth)
        updateLabel()
    }

    private fun updateLabel() {
        val myFormat = "MM/dd/yyyy"
        val sdf = SimpleDateFormat(myFormat, Locale.UK)

        et_date.setText(sdf.format(myCalendar.time))
        date=sdf.format(myCalendar.time)
    }
}