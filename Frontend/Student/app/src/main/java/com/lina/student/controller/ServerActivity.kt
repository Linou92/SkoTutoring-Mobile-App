package com.lina.student.controller

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.lina.student.R
import com.lina.student.network.ConnectionURL.Companion.MAIN_URL
import com.lina.student.network.ConnectionURL.Companion.MAIN_URL1
import kotlinx.android.synthetic.main.activity_server.*

class ServerActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_server)

        btn_go.setOnClickListener {

            MAIN_URL=tv_server.text.toString()+"/Student.HttpApi/"
            MAIN_URL1=tv_server.text.toString()+"/CoursesHttpApi/"
            val intent= Intent(this,LogInActivity::class.java)
            startActivity(intent)
        }

    }
}