package com.lina.teacher.controller

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.androidnetworking.error.ANError
import com.lina.teacher.R
import com.lina.teacher.TemplateApplication
import com.lina.teacher.model.Country
import com.lina.teacher.model.Profile
import com.lina.teacher.network.Connection
import com.lina.teacher.network.ConnectionAPI
import com.lina.teacher.network.ConnectionURL
import com.lina.teacher.util.SharedPrefacesManager
import io.reactivex.disposables.Disposable
import kotlinx.android.synthetic.main.activity_home_page.*
import org.json.JSONObject

class HomePageActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home_page)


        btn_profile.setOnClickListener {
            val intent= Intent(this,UserInfoActivity::class.java)
            startActivity(intent)
        }
        btn_Lang.setOnClickListener {
            val intent= Intent(this,LanguageActivity::class.java)
            startActivity(intent)

        }

        btn_level.setOnClickListener {
            val intent= Intent(this,LevelActivity::class.java)
            startActivity(intent)

        }
        btn_Sub.setOnClickListener {

            val intent= Intent(this,SubjectsActivity::class.java)
            startActivity(intent)

        }
        btn_avi.setOnClickListener {

            val intent= Intent(this,AvailabilityActivity::class.java)
            startActivity(intent)
        }
        btn_session.setOnClickListener {
            val intent= Intent(this,SessionsActivity::class.java)
            startActivity(intent)
        }


        btn_mlocation.setOnClickListener {

            val intent= Intent(this,MapActivity::class.java)
            startActivity(intent)
        }
    }

}