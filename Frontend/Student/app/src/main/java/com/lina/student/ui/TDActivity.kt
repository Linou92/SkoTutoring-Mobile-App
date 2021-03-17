package com.lina.student.ui

import android.os.Bundle
import android.os.PersistableBundle
import androidx.appcompat.app.AppCompatActivity
import android.content.Intent
import android.widget.Toast
import java.util.*
import com.lina.student.util.Utils
import io.reactivex.subjects.PublishSubject



open class TDActivity : AppCompatActivity() {


    val finishObserver = PublishSubject.create<Boolean>()
    var enableNotification = true

    companion object {

        var currentActivityId = ""

        const val APP_CONSTANT_TYPE = "constant_key"
        const val APP_CONSTANT_TYPE_TWO = "constant_two_key"
        const val FINISH_ACTIVITY_REQUEST_CODE = 200
        const val FINISH_ACTIVITY = 502
    }

    var activityId = "_"

    override fun onCreate(savedInstanceState: Bundle?, persistentState: PersistableBundle?) {
        super.onCreate(savedInstanceState, persistentState)
        activityId = UUID.randomUUID().toString()
    }

    override fun onResume() {
        super.onResume()
        currentActivityId = activityId
    }

    private fun isVisibleActivity(): Boolean {
        return currentActivityId == activityId
    }

    fun showToast(msg: String) {
        Toast.makeText(this, msg, Toast.LENGTH_SHORT).show()
    }

    fun hideKeyboard() {
        Utils.hideSoftKeyboard(this)
    }

    fun finishWithClosePreviousActivity() {
        val returnIntent = Intent()
        returnIntent.putExtra("finish_activity", "finish")
        setResult(FINISH_ACTIVITY, returnIntent)
        this.finish()
    }





    fun finishWithClosePreviousActivityToTaskMain() {
        val returnIntent = Intent()
        returnIntent.putExtra("finish_activity", "main_task")
        setResult(FINISH_ACTIVITY, returnIntent)
        this.finish()
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (requestCode == FINISH_ACTIVITY_REQUEST_CODE) {
            if (resultCode == FINISH_ACTIVITY) {
                val result = data?.getStringExtra("finish_activity")
                if (result == "finish") {
                    finishObserver.onNext(true)
                }  else
                    finishWithClosePreviousActivityToTaskMain()
            }
        }
    }

    override fun onStart() {
        super.onStart()

    }

    override fun onStop() {
        super.onStop()

    }


}