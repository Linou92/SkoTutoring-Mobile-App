package com.lina.student.ui


import android.os.Bundle
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.lina.student.R
import com.lina.student.util.Utils

open class TDFragment : Fragment() {

    var progressBar: View? = null
    var reloadView: View? = null
    var containerView: View? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return super.onCreateView(inflater, container, savedInstanceState)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        if (view is ViewGroup) {
            activity?.let {
                val inflater = LayoutInflater.from(it)
                val viewGroup = view
                val layout = inflater.inflate(R.layout.request_layout, viewGroup, false) as ConstraintLayout
                viewGroup.addView(layout)
                progressBar = viewGroup.findViewById<View>(R.id.progress_bar_request)
                reloadView = viewGroup.findViewById<View>(R.id.reload_connection_request)
            }
        }
    }

    fun showProgressView(value: Boolean) {
        progressBar?.visibility = if (value) View.VISIBLE else View.GONE
        containerView?.visibility = if (value) View.GONE else View.VISIBLE
//        reloadView?.visibility = if (value) View.GONE else View.VISIBLE
    }

    fun showReloadView(value: Boolean) {
        reloadView?.visibility = if (value) View.VISIBLE else View.GONE
        progressBar?.visibility = if (value) View.GONE else View.VISIBLE
    }

    fun hideAllRequestViews() {
        progressBar?.visibility = View.GONE
        reloadView?.visibility = View.GONE
    }

    fun showToast(msg: String) {
        activity?.let {
            Toast.makeText(it, msg, Toast.LENGTH_SHORT).show()
        }
    }

    fun hideKeyboard() {
        activity?.let {
            Utils.hideSoftKeyboard(it)
        }
    }


}
