package com.lina.teacher.controller

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.lina.teacher.R
import com.lina.teacher.network.ConnectionURL.Companion.MAIN_URL
import kotlinx.android.synthetic.main.activity_server.*

class ServerActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_server)

        btn_go.setOnClickListener {

            MAIN_URL=tv_server.text.toString()+"/TeacherApi/"
            val intent= Intent(this,LogInActivity::class.java)
            startActivity(intent)
        }

    }
}