###############################################################################
### https://github.com/ivanwfr/DX1Utility ######## Makefile_TAG (200722:19h:36)
###############################################################################
# VARS {{{
 ORIGIN = https://github.com/ivanwfr/DX1Utility
    JAR = jar.exe
#}}}

include Make_GIT

:cd %:h|make view_project
view_project: #{{{
	explorer $(ORIGIN)

#}}}

:cd %:h|up|only|set columns=999|vert terminal ++cols=150 make links
links: #{{{
	(\
	    mkdir DX1Utility;\
	    cd    DX1Utility;\
	    $(JAR) xvf ../DX1Utility_*.zip;\
	    )
	#}}}

###############################################################################

# vim: noet ts=8 sw=8
